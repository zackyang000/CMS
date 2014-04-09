mongoose = require('mongoose')
User = mongoose.model('User')

###
Find user by id
###
exports.user = (req, res, next, id) ->
  User.load id, (err, user) ->
    return next(err) if err
    return next(new Error("Failed to load user " + id)) unless user
    req.user = user
    next()

###
Create an user
###
exports.create = (req, res) ->
  user = new User(req.body)
  user.save (err) ->
    if err
      res.send "users/signup",
        errors: err.errors
    else
      res.jsonp user

###
Update an user
###
exports.update = (req, res) ->
  user = req.user
  user = _.extend(user, req.body)
  user.save (err) ->
    if err
      res.send "users/signup",
        errors: err.errors
    else
      res.jsonp user

###
Delete an user
###
exports.destroy = (req, res) ->
  user = req.user
  user.remove (err) ->
    if err
      res.send "users/signup",
        errors: err.errors
    else
      res.jsonp user

###
Show an user
###
exports.get = (req, res) ->
  res.jsonp req.user

###
List of users
###
exports.all = (req, res) ->
  User.find().sort("-inDate").exec (err, users) ->
    if err
      res.render "error",
        status: 500
    else
      res.jsonp users
