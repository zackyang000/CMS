var mongoose = require('mongoose'),
  User = mongoose.model('User'),
  crypto = require('crypto');

//Find user by id
exports.user = function(req, res, next, id) {
  User.load(id, function(err, user) {
    if(err) {
      next(err);
    }
    if(!user) {
      next(new Error("Failed to load user " + id));
    }
    req.user = user;
    next();
  });
};

//Create an user
exports.create = function(req, res) {
  var user = new User(req.body);
  user.save(function(err) {
    if(err) {
      res.send("users/signup", {
        errors: err.errors
      });
    } else {
      res.jsonp(user);
    }
  });
};

//Update an user
exports.update = function(req, res) {
  var user = req.user;
  user = _.extend(user, req.body);
  user.save(function(err) {
    if(err) {
      res.send("users/signup", {
        errors: err.errors
      });
    } else {
      res.jsonp(user);
    }
  });
};

//Delete an user
exports.destroy = function(req, res) {
  var user = req.user;
  return user.remove(function(err) {
    if(err) {
      res.send("users/signup", {
        errors: err.errors
      });
    } else {
      res.jsonp(user);
    }
  });
};

//Show an user
exports.get = function(req, res) {
  res.jsonp(req.user);
};

//List of users
exports.all = function(req, res) {
  User.find().sort("-inDate").exec(function(err, users) {
    if(err) {
      res.render("error", {
        status: 500
      });
    } else {
      //todo 仅返回以下字段
      /*
      name: user.name,
      loginName: user.loginName,
      email: user.email
      */
      res.jsonp(users);
    }
  });
};

exports.autoSignin = function(req, res) {

}

exports.signin = function(req, res) {
  var name = req.body.UserName;
  var pwd = req.body.Password;
  User.findOne({loginName: name, password: pwd}).exec(function(err, user) {
    if(!user) {
      return res.send(401, "Failed to login.");
    }
    user.token = crypto.createHash('md5').update(new Date() + pwd).digest('hex')
    user.save();

    res.set("token", user.token);
    res.json({
      name: user.name,
      loginName: user.loginName,
      email: user.email
    })
  });
}

exports.signout = function(req, res) {

}