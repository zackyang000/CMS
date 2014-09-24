fs = require("fs")
crypto = require("crypto")
mkdirp = require('mkdirp')
gm = require('gm')
mongoose = require("mongoose")
User = mongoose.model("User")

module.exports = (app) ->
  client = require('../odata-rest')

  client.options.set('app', app)
  client.options.set('maxTop', 10)

  client.register
    url: 'articles',
    modelName: "Article"
    options:
      defaultOrderby: 'date desc'
    actions: [

      ]

  client.register
    url: 'categories',
    modelName: "Category"
    options:
      defaultOrderby: 'date desc'

  client.register
    url: 'galleries',
    modelName: "Gallery"
    options:
      defaultOrderby: 'date desc'

  client.register
    url: 'board',
    modelName: "Board"
    options:
      defaultOrderby: 'date desc'

  client.register
    url: 'users',
    modelName: "User"

# Login, refresh user token.
  client.registerFunction
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
  client.registerFunction
    url: 'auto-login',
    method: 'POST',
    handle: (req, res, next) ->
      token = req.get("authorization")
      User.findOne(token: token).exec (err, user) ->
        unless user
          return res.send(401, "Failed to auto-login.")
        res.json
          name: user.name
          loginName: user.loginName
          email: user.email
          disabled: user.disabled

# Logout, remove user token.
  client.registerFunction
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

  client.registerFunction
    url: 'file-upload',
    method: 'POST',
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