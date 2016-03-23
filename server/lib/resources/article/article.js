'use strict';

var _models = require('../../models');

var _models2 = _interopRequireDefault(_models);

var _auth = require('../auth');

var _auth2 = _interopRequireDefault(_auth);

var _rss = require('../../services/rss');

var _rss2 = _interopRequireDefault(_rss);

var _nodeOdata = require('node-odata');

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var rssGenerate = function rssGenerate() {
  _nodeOdata.resources.article.find().sort({ date: 'desc' }).limit(50).exec(function (err, data) {
    _rss2.default.generateArticles(data);
  });
};

module.exports = (0, _nodeOdata.Resource)('article', _models2.default.article).orderBy('date desc').list().after(function (data) {
  if (data.value.length === 1) {
    var entity = data.value[0];
    entity.meta.views = entity.meta.views || 0;
    entity.meta.views++;
    entity.save();
  }
}).post().auth(_auth2.default.admin).after(rssGenerate).put().auth(_auth2.default.admin).after(rssGenerate).delete().auth(_auth2.default.admin).after(rssGenerate).action('/add-comment', function (req, res, next) {
  return _nodeOdata.resources.article.findById(req.params.id, function (err, article) {
    if (err) {
      return next(err);
    }
    if (!article) {
      return next(new Error('Failed to load article ' + req.query.id));
    }
    article.comments.push(req.body);
    article.meta.comments = article.comments.length;
    return article.save(function (err2) {
      if (err2) {
        return next(err);
      }
      res.jsonp(req.body);
      var comment = new Comment(req.body);
      comment.articleId = req.params.id;
      return comment.save(function () {
        return _nodeOdata.resources.comment.find().sort({ date: 'desc' }).limit(10).exec(function (err1, data) {
          var count = 0;
          data.map(function (item) {
            return _nodeOdata.resources.article.findById(item.articleId, function (err3, article1) {
              item.article = article1;
              count++;
              if (count === data.length) {
                _rss2.default.generateComments(data);
              }
            });
          });
        });
      });
    });
  });
});