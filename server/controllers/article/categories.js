var mongoose = require('mongoose'),
  Category = mongoose.model('Category'),
  _ = require('lodash');

// Create category.
exports.create = function(req, res, next) {
  var category = new Category(req.body);
  category.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(category);
    }
  });
};

// Update category.
exports.update = function(req, res, next) {
  Category.findOne({_id: req.params.id}, function(err, category) {
    if(err) {
      next(err);
    }
    if(!category) {
      next(new Error("Failed to load category " + req.params.id));
    }

    category = _.extend(category, req.body);

    category.save(function(err) {
      if(err) {
        next(err);
      } else {
        res.jsonp(category);
      }
    });
  });


};

// Get default category.
exports.main = function(req, res, next) {
  Category.findOne({main: true}, function(err, category) {
    if(err) {
      next(err);
    }
    if(!category) {
      next(new Error("Failed to load category."));
    }
    res.jsonp(category);
  });
};

// Show category.
exports.get = function(req, res, next) {
  Category.findOne({_id: req.params.id}, function(err, category) {
    if(err) {
      next(err);
    }
    if(!category) {
      next(new Error("Failed to load category " + req.query.id));
    }
    res.jsonp(category);
  });
};

// List of categories.
exports.all = function(req, res) {
  Category.find().sort("name").exec(function(err, category) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      res.jsonp(category);
    }
  });
};
