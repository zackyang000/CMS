fs = require("fs")
path = require("path")
appPath = process.cwd()

module.exports = (app) ->
  app.get "/admin", (req, res) ->
    res.sendfile(path.join(appPath, "../client/admin-index.html"))

  app.get "/admin(/*)", (req, res) ->
    res.sendfile(path.join(appPath, "../client/admin-index.html"))

  app.get /^(?!\/[api|oData]).*/, (req, res) ->
    res.sendfile(path.join(appPath, "../client/index.html"))