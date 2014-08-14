fs = require("fs")
mkdirp = require('mkdirp')
crypto = require('crypto')
module.exports = (app, prefix) ->
  app.post prefix + "/file-upload", (req, res) ->
    sourcePath = req.files.file.path
    fileExtension = req.files.file.name.split('.').pop()
    debugger
    filename = (req.query.name || crypto.createHash('sha1').update('' + +new Date()).digest('hex')) + '.' + fileExtension
    targetPath = "../client/upload/"+ req.query.path
    mkdirp(targetPath)
    targetPath += '/' + filename
    fs.rename sourcePath, targetPath, (err) ->
      throw err  if err
      fs.unlink sourcePath, ->
        throw err  if err
        res.send '/upload/' + req.query.path + '/' + filename
