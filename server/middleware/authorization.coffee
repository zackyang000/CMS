mongoose = require('node-odata').mongoose

module.exports = (req, res, next) ->
  User = mongoose.model("User")
  token = req.headers.authorization
  return next()  unless token
  User.findOne(token: token).exec (err, user) ->
    if user && !user.disabled
      req.user = user
    next()