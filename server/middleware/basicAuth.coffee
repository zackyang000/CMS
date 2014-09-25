mongoose = require("mongoose")
User = mongoose.model("User")

module.exports = ->
  (req, res, next) ->
    token = req.headers.authorization
    return next()  unless token
    User.findOne(token: token).exec (err, user) ->
      if user && !user.disabled
        req.user = user
      next()