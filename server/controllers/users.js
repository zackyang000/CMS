var mongoose = require('mongoose'),
  User = mongoose.model('User'),
  crypto = require('crypto'),
  _ = require('lodash');

// Create an user.
exports.create = function(req, res, next) {
  var user = new User(req.body);
  console.log(user);
  user.save(function(err) {
    if(err) {
      next(err);
    }
    else {
      res.jsonp(user);
    }
  });
};

// Update an user.
exports.update = function(req, res, next) {
  User.findOne(req.query.id, function(err, user) {
    if(err) {
      next(err);
    }
    if(!user) {
      next(new Error("Failed to load user " + id));
    }
    console.log(user);
    console.log(req.body);
    user = _.extend(user, req.body);
    console.log(user);

    user.save(function(err) {
      if(err) {
        res.send("users/signup", {
          errors: err.errors
        });
      } else {
        res.jsonp(user);
      }
    });
  });


};

// Show an user.
exports.get = function(req, res) {
  User.findOne(req.query.id, function(err, user) {
    if(err) {
      next(err);
    }
    if(!user) {
      next(new Error("Failed to load user " + id));
    }
    res.jsonp({
      _id: user._id,
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      disabled: user.disabled
    });
  });
};

// List of users.
exports.all = function(req, res) {
  User.find().sort("-inDate").exec(function(err, users) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      res.send(JSON.stringify(users, ["_id", "name", "loginName", "email", "disabled"]));
    }
  });
};

// Auto-login valid by user token.
exports.autoLogin = function(req, res) {
  var token = req.get("authorization");
  User.findOne({token: token}).exec(function(err, user) {
    if(!user) {
      return res.send(401, "Failed to auto-login.");
    }
    res.json({
      _id: user._id,
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      disabled: user.disabled
    })
  });
}

// Login, refresh user token.
exports.login = function(req, res) {
  var name = req.body.name;
  var pwd = req.body.password;
  User.findOne({loginName: name, password: pwd}).exec(function(err, user) {
    if(!user) {
      return res.send(401, "Failed to login.");
    }
    user.token = crypto.createHash('md5').update(new Date() + pwd).digest('hex')
    user.save();
    res.set("authorization", user.token);

    res.json({
      _id: user._id,
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      disabled: user.disabled
    })
  });
}

// Logout, remove user token.
exports.logout = function(req, res) {
  var token = req.get("authorization");
  User.findOne({token: token}).exec(function(err, user) {
    if(!user) {
      return res.send(400, "User not found.");
    }
    user.token = null
    user.save();

    res.json({
      _id: user._id,
      name: user.name,
      loginName: user.loginName,
      email: user.email,
      disabled: user.disabled
    })
  });
}