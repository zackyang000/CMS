mongoose = require("mongoose")
User = mongoose.model("User")
crypto = require("crypto")
_ = require("lodash")


# Create an user.
exports.create = (req, res, next) ->
  user = new User(req.body)
  user.save (err) ->
    if err
      next(err)
    res.jsonp(user)


# Update an user.
exports.update = (req, res, next) ->
  User.findOne
    loginName: req.params.id
  , (err, user) ->
    if err
      next(err)
    unless user
      next(new Error("Failed to load user " + req.params.id))
    user = _.extend(user, req.body)
    user.save (err) ->
      if err
        next(err)
      res.jsonp user


# Show an user.
exports.get = (req, res) ->
  User.findOne
    loginName: req.params.id
  , (err, user) ->
    if err
      next(err)
    unless user
      next(new Error("Failed to load user " + req.query.id))
    res.jsonp
      name: user.name
      loginName: user.loginName
      email: user.email
      disabled: user.disabled


# List of users.
exports.all = (req, res) ->
  User.find().sort("name").exec (err, users) ->
    if err
      res.render "error",
        status: 500
    res.send JSON.stringify(users, [
      "name"
      "loginName"
      "email"
      "disabled"
    ])

# Auto-login valid by user token.
exports.autoLogin = (req, res) ->
  token = req.get("authorization")
  User.findOne(token: token).exec (err, user) ->
    unless user
      return res.send(401, "Failed to auto-login.")
    res.json
      name: user.name
      loginName: user.loginName
      email: user.email
      disabled: user.disabled


# Login, refresh user token.
exports.login = (req, res) ->
  name = req.body.name
  pwd = req.body.password
  User.findOne(
    loginName: name
    password: pwd
  ).exec (err, user) ->
    unless user
      return res.send(401, "Failed to login.")
    user.token = crypto.createHash("md5").update(new Date() + pwd).digest("hex")
    user.save()
    res.set("authorization", user.token)
    res.json
      name: user.name
      loginName: user.loginName
      email: user.email
      disabled: user.disabled


# Logout, remove user token.
exports.logout = (req, res) ->
  token = req.get("authorization")
  User.findOne(token: token).exec (err, user) ->
    unless user
      return res.send(400, "User not found.")
    user.token = null
    user.save()
    res.json
      name: user.name
      loginName: user.loginName
      email: user.email
      disabled: user.disabled