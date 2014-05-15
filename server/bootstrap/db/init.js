var fs = require("fs");
var appPath = process.cwd();
var mongoose = require('mongoose');

var initData = function(model, path) {
  require(appPath + path).forEach(function(item) {
    var user = new model(item);
    user.save();
  })
}

module.exports = function() {
  //initData(mongoose.model('User'), '/bootstrap/db/system/user.json');
  initData(mongoose.model('Article'), '/bootstrap/db/article/article.json');
  initData(mongoose.model('Category'), '/bootstrap/db/article/category.json');
  initData(mongoose.model('Comment'), '/bootstrap/db/common/comment.json');
  initData(mongoose.model('Tag'), '/bootstrap/db/common/tag.json');
  initData(mongoose.model('Gallery'), '/bootstrap/db/photo/gallery.json');
  initData(mongoose.model('Photo'), '/bootstrap/db/photo/photo.json');
}