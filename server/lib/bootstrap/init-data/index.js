'use strict';

var _nodeOdata = require('node-odata');

var initData = function initData(model, path) {
  require(path).forEach(function (item) {
    var data = new model(item);
    data.save(function (err) {
      if (err) {
        console.log(err);
      }
    });
    console.log('data init: ' + path + ' import successful.');
  });
};

module.exports = {
  import: function _import() {
    _nodeOdata.resources.user.find().exec(function (err, users) {
      if (!users.length) {
        initData(_nodeOdata.resources.user, './system/user.json');
        initData(_nodeOdata.resources.article, './article/article.json');
        initData(_nodeOdata.resources.category, './article/category.json');
        initData(_nodeOdata.resources.board, './board/board.json');
        initData(_nodeOdata.resources.gallery, './photo/gallery.json');
      }
    });
  }
};