fs = require("fs")
mkdirp = require('mkdirp')
crypto = require('crypto')
module.exports = (app, prefix) ->
  app.post prefix + "/file-upload", (req, res) ->
    sourcePath = req.files.file.path
    filename = crypto.createHash('sha1').update('' + +new Date()).digest('hex') + '.' + req.files.file.name.split('.').pop()
    targetPath = "../client/upload/"+ req.query.path
    mkdirp(targetPath)
    targetPath += '/' + filename
    fs.rename sourcePath, targetPath, (err) ->
      throw err  if err
      fs.unlink sourcePath, ->
        throw err  if err
        res.send targetPath
