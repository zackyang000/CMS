'use strict';

var _nodeOdata = require('node-odata');

var _nodeOdata2 = _interopRequireDefault(_nodeOdata);

var _cors = require('cors');

var _cors2 = _interopRequireDefault(_cors);

var _path = require('path');

var _path2 = _interopRequireDefault(_path);

var _errorhandler = require('errorhandler');

var _errorhandler2 = _interopRequireDefault(_errorhandler);

var _morgan = require('morgan');

var _morgan2 = _interopRequireDefault(_morgan);

var _bodyParser = require('body-parser');

var _bodyParser2 = _interopRequireDefault(_bodyParser);

var _authorization = require('./middleware/authorization');

var _authorization2 = _interopRequireDefault(_authorization);

var _article = require('./resources/article/article');

var _article2 = _interopRequireDefault(_article);

var _category = require('./resources/article/category');

var _category2 = _interopRequireDefault(_category);

var _comment = require('./resources/article/comment');

var _comment2 = _interopRequireDefault(_comment);

var _gallery = require('./resources/gallery/gallery');

var _gallery2 = _interopRequireDefault(_gallery);

var _board = require('./resources/board/board');

var _board2 = _interopRequireDefault(_board);

var _user = require('./resources/system/user');

var _user2 = _interopRequireDefault(_user);

var _login = require('./functions/login');

var _login2 = _interopRequireDefault(_login);

var _upload = require('./functions/upload');

var _upload2 = _interopRequireDefault(_upload);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var server = (0, _nodeOdata2.default)('mongodb://localhost/cms');

// hack: persistence current all resouces for actions and functions to use.
_nodeOdata2.default.resources = server.resources;

// odata config
server.use((0, _cors2.default)({ exposedHeaders: 'authorization' }));
server.use((0, _bodyParser2.default)({
  uploadDir: _path2.default.join(_path2.default.dirname(__dirname), 'server/static/upload/temp')
}));
server.use(_nodeOdata2.default._express.static(_path2.default.join(__dirname, './static')));
server.use(_authorization2.default);
server.use((0, _morgan2.default)('short'));
server.use((0, _errorhandler2.default)());

// init resources
[_article2.default, _category2.default, _comment2.default, _gallery2.default, _board2.default, _user2.default].map(function (resource) {
  return server.use(resource);
});

// init functions
[_login2.default, _upload2.default].map(function (resource) {
  return server.use(resource);
});

// start web server
server.listen(process.env.PORT || 40002, function () {
  return(
    // import data
    require('./bootstrap/init-data').import()
  );
});