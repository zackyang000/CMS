resources = require('node-odata').resources

module.exports = (req, res, next) ->
  token = req.headers.authorization
  return next()  unless token
  resources.user.findOne({token: token}).exec (err, user) ->
    if user && !user.disabled
      req.user = user
    next()

