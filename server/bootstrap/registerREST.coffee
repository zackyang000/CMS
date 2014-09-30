fs = require("fs")
crypto = require("crypto")
mkdirp = require('mkdirp')
gm = require('gm')
mongoose = require("mongoose")
User = mongoose.model("User")
Article = mongoose.model("Article")

module.exports = (app) ->
  odata = require('../odata-rest')

  odata.options.set('app', app)
  odata.options.set('maxTop', 10)

  authAdmin = (req, res) -> !!req.user

  odata.register
    url: 'articles',
    modelName: "Article"
    options:
      defaultOrderby: 'date desc'
    auth:
      "POST,PUT,DELETE": authAdmin
    actions: [
      url: 'add-comment'
      handle: (req, res, next) ->
        Article.findOne
          _id: req.params.id
        , (err, article) ->
          if err
            next(err)
          unless article
            next new Error("Failed to load article " + req.query.id)
          article.comments.push(req.body)
          article.save (err) ->
            if err
              next(err)
            res.jsonp(req.body)
    ]

  odata.register
    url: 'categories',
    modelName: "Category"
    options:
      defaultOrderby: 'date desc'
    auth:
      "POST,PUT,DELETE": authAdmin

  odata.register
    url: 'galleries',
    modelName: "Gallery"
    options:
      defaultOrderby: 'date desc'
    auth:
      "POST,PUT,DELETE": authAdmin

  odata.register
    url: 'board',
    modelName: "Board"
    options:
      defaultOrderby: 'date desc'
    auth:
      "DELETE,PUT": authAdmin

  odata.register
    url: 'users',
    modelName: "User"
    auth:
      "POST,PUT,DELETE": authAdmin


# Login, refresh user token.
  odata.registerFunction
    url: 'login',
    method: 'POST',
    handle: (req, res, next) ->
      name = req.body.name
      pwd = req.body.password
      User.findOne(
        password: pwd
      ).or([{ loginName: name }, { email: name }]).exec (err, user) ->
        unless user
          return res.send(401, "Failed to login.")
        user.token = crypto.createHash("md5").update(new Date() + pwd).digest("hex")
        user.save()
        res.set("authorization", user.token)
        res.json
          name: user.name
          loginName: user.loginName
          email: user.email
          disabled: user.disabled


# Auto-login valid by user token.
  odata.registerFunction
    url: 'auto-login',
    method: 'POST',
    handle: (req, res, next) ->
      return res.send(401, "Failed to auto-login.")  unless req.user
      res.json
        name: req.user.name
        loginName: req.user.loginName
        email: req.user.email
        disabled: req.user.disabled


# Logout, remove user token.
  odata.registerFunction
    url: 'logout',
    method: 'POST',
    handle: (req, res, next) ->
      token = req.get("authorization")
      User.findOne(token: token).exec (err, user) ->
        unless user
          return res.send(400, "User not found.")
        user.token = null
        user.save()
        res.json
          name: user.name
          loginName: user.loginName
          email: user.email
          disabled: user.disabled


  odata.registerFunction
    url: 'file-upload',
    method: 'POST',
    auth: authAdmin
    handle: (req, res, next) ->
      sourcePath = req.files.file.path
      targetFolder = "../client/upload/"+ req.query.path
      mkdirp(targetFolder)
      filename = req.query.name || crypto.createHash('sha1').update('' + +new Date()).digest('hex')
      fileExtension = req.files.file.name.split('.').pop()
      targetPath = targetFolder + '/' + filename + '.' + fileExtension

      #缩略图
      if req.query.thumbnail
        size = req.query.thumbnail.split('x')
        gm(sourcePath)
        .resize(size[0] || null, size[1] || null, '@')
        .noProfile()
        .write targetFolder + '/' + filename + '.thumbnail.' + fileExtension, ->

      #裁剪
      complated = (err) ->
        throw err  if err
        fs.unlink sourcePath, ->
          throw err  if err
      if req.query.resize
        size = req.query.resize.split('x')
        gm(sourcePath)
        .resize(size[0] || null, size[1] || null, '@')
        .noProfile()
        .write targetPath, complated
      else
        fs.rename sourcePath, targetPath, complated

      res.send '/upload/' + req.query.path + '/' + filename + '.' + fileExtension