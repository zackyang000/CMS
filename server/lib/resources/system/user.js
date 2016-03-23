'use strict';

var _nodeOdata = require('node-odata');

var _models = require('../../models');

var _models2 = _interopRequireDefault(_models);

var _auth = require('../auth');

var _auth2 = _interopRequireDefault(_auth);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

module.exports = (0, _nodeOdata.Resource)('user', _models2.default.user).orderBy('date desc').list().auth(_auth2.default.admin).get().auth(_auth2.default.admin).post().auth(_auth2.default.admin).delete().auth(_auth2.default.admin).put().auth(_auth2.default.admin).after(function (newEntity, oldEntity) {
  return _nodeOdata.resources.article.find({ 'meta.author': oldEntity.name }).exec(function (err, articles) {
    articles.map(function (article) {
      article.meta.author = newEntity.name;
      return article.save();
    });
  });
});