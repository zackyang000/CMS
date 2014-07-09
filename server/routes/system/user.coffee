users = require("../../controllers/system/users")

module.exports = (app, prefix) ->
  #REST
  app.get prefix + "/users", users.all
  app.post prefix + "/users", users.create
  app.get prefix + "/users/:id", users.get
  app.put prefix + "/users/:id", users.update

  #ACTION
  app.post prefix + "/users/:id/auto-login", users.autoLogin
  app.post prefix + "/users/:id/login", users.login
  app.post prefix + "/users/:id/logout", users.logout