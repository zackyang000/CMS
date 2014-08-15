fs = require("fs")
mkdirp = require('mkdirp')
crypto = require('crypto')
gm = require('gm')

module.exports = (app, prefix) ->
  app.post prefix + "/file-upload", (req, res) ->
    sourcePath = req.files.file.path
    debugger
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