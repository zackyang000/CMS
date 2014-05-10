var fs = require("fs");
var appPath = process.cwd();
var mongoose = require('mongoose');
var User = mongoose.model('User');

var initUser = function() {
  require(appPath + '/bootstrap/db/users.json').forEach(function(item) {
    var user = new User(item);
    user.save();
  })
}

module.exports = function() {
  //initUser();
}