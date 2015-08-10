crypto = require("crypto")
func = require('node-odata').Function
resources = require('node-odata').resources

router = func()

router.post '/login', (req, res, next) ->
  name = req.body.name
  pwd = req.body.password
  resources.user.findOne({password: pwd}).or([{ loginName: name }, { email: name }]).exec (err, user) ->
    unless user
      return res.send(401, "Failed to login.")
    user.token = crypto.createHash("md5").update(new Date() + pwd).digest("hex")
    user.save()
    res.set("authorization", user.token)
    res.json
      name: user.name
      loginName: user.loginName
      email: user.email
      gravatar: user.gravatar
      disabled: user.disabled

router.post '/auto-login', (req, res, next) ->
  return res.send(401, "Failed to auto-login.")  unless req.user
  res.json
    name: req.user.name
    loginName: req.user.loginName
    email: req.user.email
    gravatar: req.user.gravatar
    disabled: req.user.disabled

router.post '/logoff', (req, res, next) ->
  token = req.get("authorization")
  resources.user.findOne(token: token).exec (err, user) ->
    unless user
      return res.send(400, "User not found.")
    user.token = null
    user.save()
    res.json
      name: user.name
      loginName: user.loginName
      email: user.email
      gravatar: user.gravatar
      disabled: user.disabled

module.exports = router
