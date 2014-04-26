var mongoose = require('mongoose'),
User = mongoose.model('User');


/*
Find user by id
 */

exports.user = function(req, res, next, id) {
  return User.load(id, function(err, user) {
    if (err) {
      return next(err);
    }
    if (!user) {
      return next(new Error("Failed to load user " + id));
    }
    req.user = user;
    return next();
  });
};


/*
Create an user
 */

exports.create = function(req, res) {
  var user;
  user = new User(req.body);
  return user.save(function(err) {
    if (err) {
      return res.send("users/signup", {
        errors: err.errors
      });
    } else {
      return res.jsonp(user);
    }
  });
};


/*
Update an user
 */

exports.update = function(req, res) {
  var user;
  user = req.user;
  user = _.extend(user, req.body);
  return user.save(function(err) {
    if (err) {
      return res.send("users/signup", {
        errors: err.errors
      });
    } else {
      return res.jsonp(user);
    }
  });
};


/*
Delete an user
 */

exports.destroy = function(req, res) {
  var user;
  user = req.user;
  return user.remove(function(err) {
    if (err) {
      return res.send("users/signup", {
        errors: err.errors
      });
    } else {
      return res.jsonp(user);
    }
  });
};


/*
Show an user
 */

exports.get = function(req, res) {
  return res.jsonp(req.user);
};


/*
List of users
 */

exports.all = function(req, res) {
  return User.find().sort("-inDate").exec(function(err, users) {
    if (err) {
      return res.render("error", {
        status: 500
      });
    } else {
      return res.jsonp(users);
    }
  });
};
