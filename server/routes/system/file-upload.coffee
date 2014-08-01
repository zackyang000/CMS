fs = require("fs")
mkdirp = require('mkdirp')

module.exports = (app, prefix) ->
  app.post prefix + "/file-upload", (req, res) ->
    tmpPath = req.files.file.path
    filename = req.files.file.path.split('\\').pop() + '.' + req.files.file.name.split('.').pop()
    targetPath = "../client/upload/"+ req.query.path
    mkdirp(targetPath)
    targetPath += '/' + filename
    debugger
    fs.rename tmpPath, targetPath, (err) ->
      throw err  if err

      fs.unlink tmpPath, ->
        throw err  if err
        res.send targetPath
