express = require("express")
path = require("path")
appPath = process.cwd()

module.exports = (app) ->
  app.use express.favicon(path.join(appPath, '../client/img/favicon.ico'))
  app.use express.static(path.join(appPath, "../client"))

  app.get('/admin(/*)', (req, res) ->
    res.sendfile(path.join(appPath, '../client/admin-index.html'))
  )

  app.get(/^(?!\/api\/).*/, (req, res) ->
    res.sendfile(path.join(appPath, '../client/index.html'))
  )