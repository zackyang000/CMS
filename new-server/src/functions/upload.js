import { Function as func } from 'node-odata';
import { resources } from 'node-odata';
import gm from 'gm';
import fs from 'fs';
import mkdirp from 'mkdirp';

router = func();

router.post('/file-upload', (req, res, next) => {
  const sourcePath = req.files.file.path;
  const targetFolder = "./static/upload/" + req.query.path;
  mkdirp(targetFolder);
  const filename = req.query.name || crypto.createHash('sha1').update('' + +new Date()).digest('hex');
  const fileExtension = req.files.file.name.split('.').pop();
  const targetPath = targetFolder + '/' + filename + '.' + fileExtension;

  // 缩略图
  if(req.query.thumbnail) {
    const [ width, height ] = req.query.thumbnail.split('x');
    gm(sourcePath)
    .resize(width, height, '^')
    .gravity('Center')
    .crop(width, height)
    .autoOrient()
    .noProfile()
    .write(targetFolder + '/' + filename + '.thumbnail.' + fileExtension, () => {});
  }

  complated = (err) => {
    if (err) {
      throw err;
    }
    fs.unlink(sourcePath, () => {
      if (err) {
        throw err;
      }
    });
  };

  // 缩放
  if(req.query.resize) {
    const [ width, height ] = req.query.resize.split('x');
    gm(sourcePath)
    .resize(width, height, '@')
    .autoOrient()
    .noProfile()
    .write(targetPath, complated);
  } else {
    // 直接保存
    fs.rename(sourcePath, targetPath, complated);
  }

  res.set("Connection", 'keep-alive'):
  res.send('/upload/' + req.query.path + '/' + filename + '.' + fileExtension);
});

module.exports = router;
