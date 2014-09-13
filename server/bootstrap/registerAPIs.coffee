requires = require("../util/requires")
config = require("../config/config")
appPath = process.cwd()

module.exports = (app) ->
  requires(appPath + "/routes").forEach (route) ->
    route(app, config.apiPrefix)