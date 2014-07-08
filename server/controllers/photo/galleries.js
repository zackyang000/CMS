var mongoose = require('mongoose'),
  Gallery = mongoose.model('Gallery'),
  crypto = require('crypto'),
  _ = require('lodash');

// Create an gallery.
exports.create = function(req, res, next) {
  var gallery = new Gallery(req.body);
  gallery.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(gallery);
    }
  });
};

// Update an gallery.
exports.update = function(req, res, next) {
  Gallery.findOne({name: req.params.id}, function(err, gallery) {
    if(err) {
      next(err);
    }
    if(!gallery) {
      next(new Error("Failed to load gallery " + req.params.id));
    }

    gallery = _.extend(gallery, req.body);

    gallery.save(function(err) {
      if(err) {
        next(err);
      } else {
        res.jsonp(gallery);
      }
    });
  });


};

// Show an gallery.
exports.get = function(req, res) {
  Gallery.findOne({loginName: req.params.id}, function(err, gallery) {
    if(err) {
      next(err);
    }
    if(!gallery) {
      next(new Error("Failed to load gallery " + req.query.id));
    }
    res.jsonp(gallery);
  });
};

// List of galleries.
exports.all = function(req, res) {
  Gallery.find().sort("date").exec(function(err, galleries) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      res.send(galleries);
    }
  });
};

// Delete gallery.
exports.del = function(req, res, next) {

};