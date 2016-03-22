'use strict';

var _fs = require('fs');

var _fs2 = _interopRequireDefault(_fs);

var _path = require('path');

var _path2 = _interopRequireDefault(_path);

var _rss = require('rss');

var _rss2 = _interopRequireDefault(_rss);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.generateArticles = function (list) {
  var feed = new _rss2.default({
    title: 'Zack Yang - Articles',
    description: 'Zack Yang - Articles',
    feed_url: 'http://feed.zackyang.com/articles.xml',
    site_url: 'http://zackyang.com',
    image_url: 'http://zackyang.com/img/favicon.png',
    managingEditor: 'Zack Yang',
    webMaster: 'Zack Yang',
    copyright: '2014 Zack Yang',
    language: 'cn',
    pubDate: new Date(),
    ttl: '60'
  });

  list.map(function (item) {
    return feed.item({
      title: item.title,
      description: item.content,
      url: 'http://zackyang.com/post/' + item.url,
      guid: item._id,
      author: item.meta.author,
      date: item.date
    });
  });

  var xml = feed.xml();

  _fs2.default.writeFile(_path2.default.join(_path2.default.dirname(__dirname), 'static/articles.xml'), xml);
};

exports.generateComments = function (list) {
  var feed = new _rss2.default({
    title: 'Zack Yang - Comments',
    description: 'Zack Yang - Comments',
    feed_url: 'http://feed.zackyang.com/comments.xml',
    site_url: 'http://zackyang.com',
    image_url: 'http://zackyang.com/img/favicon.png',
    managingEditor: 'Zack Yang',
    webMaster: 'Zack Yang',
    copyright: '2014 Zack Yang',
    language: 'cn',
    pubDate: new Date(),
    ttl: '60'
  });

  list.map(function (item) {
    return feed.item({
      title: item.article.title,
      description: item.content,
      url: 'http://zackyang.com/post/' + item.article.url,
      guid: item._id,
      author: item.author.name,
      date: item.date
    });
  });

  var xml = feed.xml();

  _fs2.default.writeFile(_path2.default.join(_path2.default.dirname(__dirname), 'static/comments.xml'), xml);
};