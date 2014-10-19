odata = require('node-odata')
fs = require("fs")
crypto = require("crypto")
mkdirp = require('mkdirp')
gm = require('gm')
mongoose = odata.mongoose

articleModel = require('../models/article/article')
categoryModel = require('../models/article/category')
boardModel = require('../models/board/board')
tagModel = require('../models/article/tag')
galleryModel = require('../models/gallery/gallery')
userModel = require('../models/system/user')
module.exports = (app) ->
  odata.set('app', app)
  odata.set('db', 'mongodb://localhost/cms-dev')

  authAdminFn = (req) -> !!req.user

  odata.resources.register
    url: '/articles'
    model: articleModel
    modelName: 'Article'
    options:
      defaultOrderby: 'date desc'
      maxTop: 10
    auth:
      "POST,PUT,DELETE": authAdminFn
    actions: [
      url: '/add-comment'
      handle: (req, res, next) ->
        Article = mongoose.model("Article")
        Article.findOne
          _id: req.params.id
        , (err, article) ->
          if err
            next(err)
          unless article
            next new Error("Failed to load article " + req.query.id)
          article.comments.push(req.body)
          article.meta.comments = article.meta.comments || 0
          article.meta.comments++
          article.save (err) ->
            if err
              next(err)
            res.jsonp(req.body)
    ,
      url: '/browsed'
      handle: (req, res, next) ->
        Article = mongoose.model("Article")
        Article.findOne
          _id: req.params.id
        , (err, article) ->
          if err
            next(err)
          unless article
            next new Error("Failed to load article " + req.query.id)
          article.meta.views++
          article.save (err) ->
            if err
              next(err)
            res.send(204)
    ]

  odata.resources.register
    url: '/categories'
    model: categoryModel
    modelName: 'Category'
    options:
      defaultOrderby: 'date desc'
    auth:
      "POST,PUT,DELETE": authAdminFn

  odata.resources.register
    url: '/galleries'
    model: galleryModel
    modelName: "Gallery"
    options:
      defaultOrderby: 'date desc'
    auth:
      "POST,PUT,DELETE": authAdminFn

  odata.resources.register
    url: '/board'
    model: boardModel
    modelName: "Board"
    options:
      defaultOrderby: 'date desc'
    auth:
      "DELETE,PUT": authAdminFn

  odata.resources.register
    url: '/users'
    model: userModel
    modelName: "User"
    auth:
      "POST,PUT,DELETE": authAdminFn


# Login, refresh user token.
  odata.functions.register
    url: '/login'
    method: 'POST'
    handle: (req, res, next) ->
      User = mongoose.model("User")
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
  odata.functions.register
    url: '/auto-login'
    method: 'POST'
    handle: (req, res, next) ->
      return res.send(401, "Failed to auto-login.")  unless req.user
      res.json
        name: req.user.name
        loginName: req.user.loginName
        email: req.user.email
        disabled: req.user.disabled


# Logout, remove user token.
  odata.functions.register
    url: '/logout'
    method: 'POST'
    handle: (req, res, next) ->
      User = mongoose.model("User")
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


  odata.functions.register
    url: '/file-upload'
    method: 'POST'
    auth: authAdminFn
    handle: (req, res, next) ->
      sourcePath = req.files.file.path
      targetFolder = "./static/upload/" + req.query.path
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