mongoose = require('node-odata').mongoose
repository = require('../repositories')

module.exports = (req, res, next) ->
  token = req.headers.authorization
  return next()  unless token
  repository.get('user').findOne({token: token}).exec (err, user) ->
    if user && !user.disabled
      req.user = user
    next()

