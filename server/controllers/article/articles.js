var mongoose = require('mongoose'),
  Article = mongoose.model('Article'),
  _ = require('lodash');

// Create article.
exports.create = function(req, res, next) {
  var article = new Article(req.body);
  article.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(article);
    }
  });
};

// Update article.
exports.update = function(req, res, next) {
  Article.findOne({_id: req.params.id}, function(err, article) {
    if(err) {
      next(err);
    }
    if(!article) {
      next(new Error("Failed to load article " + req.params.id));
    }

    article = _.extend(article, req.body);

    article.save(function(err) {
      if(err) {
        next(err);
      } else {
        res.jsonp(article);
      }
    });
  });
};

// Show article.
exports.get = function(req, res) {
  Article.findOne({url: req.params.id}, function(err, article) {
    if(err) {
      next(err);
    }
    if(!article) {
      next(new Error("Failed to load article " + req.query.id));
    }
    res.jsonp(article);
  });
};

// List of articles.
exports.all = function(req, res) {
  Article.find().sort("-date").exec(function(err, article) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      res.jsonp(article);
    }
  });
};
