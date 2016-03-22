'use strict';

var _article = require('./article/article');

var _article2 = _interopRequireDefault(_article);

var _comment = require('./article/comment');

var _comment2 = _interopRequireDefault(_comment);

var _category = require('./article/category');

var _category2 = _interopRequireDefault(_category);

var _board = require('./board/board');

var _board2 = _interopRequireDefault(_board);

var _tag = require('./article/tag');

var _tag2 = _interopRequireDefault(_tag);

var _gallery = require('./gallery/gallery');

var _gallery2 = _interopRequireDefault(_gallery);

var _user = require('./system/user');

var _user2 = _interopRequireDefault(_user);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

module.exports = {
  article: _article2.default,
  comment: _comment2.default,
  category: _category2.default,
  board: _board2.default,
  tag: _tag2.default,
  gallery: _gallery2.default,
  user: _user2.default
};