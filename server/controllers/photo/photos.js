var mongoose = require('mongoose');
var Photo = mongoose.model('Photo');

// Create an photo.
exports.create = function(req, res, next) {
  var photo = new Photo(req.body);
  photo.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(photo);
    }
  });
};

// Delete photo.
exports.del = function(req, res, next) {

};