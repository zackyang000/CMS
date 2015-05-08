fs = require("fs")
crypto = require("crypto")
mkdirp = require('mkdirp')
gm = require('gm')
auth = require('./auth')
repository = require('../repositories')

module.exports = (server) ->
  # Login, refresh user token.
  server.functions.register
    url: '/login'
    method: 'POST'
    handle: (req, res, next) ->
      name = req.body.name
      pwd = req.body.password
      repository.get('user').findOne({password: pwd}).or([{ loginName: name }, { email: name }]).exec (err, user) ->
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
  server.functions.register
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
  server.functions.register
    url: '/logoff'
    method: 'POST'
    handle: (req, res, next) ->
      token = req.get("authorization")
      repository.get('user').findOne(token: token).exec (err, user) ->
        unless user
          return res.send(400, "User not found.")
        user.token = null
        user.save()
        res.json
          name: user.name
          loginName: user.loginName
          email: user.email
          disabled: user.disabled

  server.functions.register
    url: '/file-upload'
    method: 'POST'
    auth: auth.admin
    handle: (req, res, next) ->
      sourcePath = req.files.file.path
      targetFolder = "./static/upload/" + req.query.path
      mkdirp(targetFolder)
      filename = req.query.name || crypto.createHash('sha1').update('' + +new Date()).digest('hex')
      fileExtension = req.files.file.name.split('.').pop()
      targetPath = targetFolder + '/' + filename + '.' + fileExtension

      #缩略图
      if req.query.thumbnail
        [ width, height ] = req.query.thumbnail.split('x')
        gm(sourcePath)
        .resize(width, height, '^')
        .gravity('Center')
        .crop(width, height)
        .autoOrient()
        .noProfile()
        .write targetFolder + '/' + filename + '.thumbnail.' + fileExtension, ->


      complated = (err) ->
        throw err  if err
        fs.unlink sourcePath, ->
          throw err  if err
      #缩放
      if req.query.resize
        [ width, height ] = req.query.resize.split('x')
        gm(sourcePath)
        .resize(width, height, '@')
        .autoOrient()
        .noProfile()
        .write targetPath, complated
      #直接保存
      else
        fs.rename sourcePath, targetPath, complated

      res.set("Connection", 'keep-alive')
      res.send '/upload/' + req.query.path + '/' + filename + '.' + fileExtension
