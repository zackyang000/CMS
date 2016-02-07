fs = require("fs")
mkdirp = require('mkdirp')
gm = require('gm')
func = require('node-odata').Function
resources = require('node-odata').resources

router = func()

router.post '/file-upload', (req, res, next) ->
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

module.exports = router
