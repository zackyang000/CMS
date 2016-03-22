'use strict';

var _slicedToArray = function () { function sliceIterator(arr, i) { var _arr = []; var _n = true; var _d = false; var _e = undefined; try { for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) { _arr.push(_s.value); if (i && _arr.length === i) break; } } catch (err) { _d = true; _e = err; } finally { try { if (!_n && _i["return"]) _i["return"](); } finally { if (_d) throw _e; } } return _arr; } return function (arr, i) { if (Array.isArray(arr)) { return arr; } else if (Symbol.iterator in Object(arr)) { return sliceIterator(arr, i); } else { throw new TypeError("Invalid attempt to destructure non-iterable instance"); } }; }();

var _nodeOdata = require('node-odata');

var _gm = require('gm');

var _gm2 = _interopRequireDefault(_gm);

var _fs = require('fs');

var _fs2 = _interopRequireDefault(_fs);

var _mkdirp = require('mkdirp');

var _mkdirp2 = _interopRequireDefault(_mkdirp);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var router = (0, _nodeOdata.Function)();

router.post('/file-upload', function (req, res) {
  var sourcePath = req.files.file.path;
  var targetFolder = './static/upload/' + req.query.path;
  (0, _mkdirp2.default)(targetFolder);
  var filename = req.query.name || crypto.createHash('sha1').update(+new Date()).digest('hex');
  var fileExtension = req.files.file.name.split('.').pop();
  var targetPath = targetFolder + '/' + filename + '.' + fileExtension;

  // 缩略图
  if (req.query.thumbnail) {
    var _req$query$thumbnail$ = req.query.thumbnail.split('x');

    var _req$query$thumbnail$2 = _slicedToArray(_req$query$thumbnail$, 2);

    var width = _req$query$thumbnail$2[0];
    var height = _req$query$thumbnail$2[1];

    (0, _gm2.default)(sourcePath).resize(width, height, '^').gravity('Center').crop(width, height).autoOrient().noProfile().write(targetFolder + '/' + filename + '.thumbnail.' + fileExtension, function () {});
  }

  var complated = function complated(err) {
    if (err) {
      throw err;
    }
    _fs2.default.unlink(sourcePath, function () {
      if (err) {
        throw err;
      }
    });
  };

  // 缩放
  if (req.query.resize) {
    var _req$query$resize$spl = req.query.resize.split('x');

    var _req$query$resize$spl2 = _slicedToArray(_req$query$resize$spl, 2);

    var _width = _req$query$resize$spl2[0];
    var _height = _req$query$resize$spl2[1];

    (0, _gm2.default)(sourcePath).resize(_width, _height, '@').autoOrient().noProfile().write(targetPath, complated);
  } else {
    // 直接保存
    _fs2.default.rename(sourcePath, targetPath, complated);
  }

  res.set('Connection', 'keep-alive');
  res.send('/upload/' + req.query.path + '/' + filename + '.' + fileExtension);
});

module.exports = router;