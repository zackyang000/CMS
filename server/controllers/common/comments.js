var mongoose = require('mongoose'),
  Comment = mongoose.model('Comment'),
  _ = require('lodash');

// Create comment.
exports.create = function(req, res, next) {
  var comment = new Comment(req.body);
  comment.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(comment);
    }
  });
};

// Update comment.
exports.update = function(req, res, next) {
  Comment.findOne({_id: req.params.id}, function(err, comment) {
    if(err) {
      next(err);
    }
    if(!comment) {
      next(new Error("Failed to load comment " + req.params.id));
    }

    comment = _.extend(comment, req.body);

    comment.save(function(err) {
      if(err) {
        next(err);
      } else {
        res.jsonp(comment);
      }
    });
  });
};

// Show comment.
exports.get = function(req, res, next) {
  var params = req.params.id.split('/')
  var query = { type: params[0] };
  if (params.length === 2){
    query.linkId = params[1];
  }

  Comment.find(query, function(err, comment) {
    if(err) {
      next(err);
    }
    if(!comment) {
      next(new Error("Failed to load comment " + req.query.id));
    }
    res.jsonp(comment);
  });
};

// List of comments.
exports.all = function(req, res) {
  Comment.find().sort("date").exec(function(err, comment) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      res.jsonp(comment);
    }
  });
};
