var requires = require("../util/requires"),
  config = require("../config/config"),
  appPath = process.cwd();

module.exports = function (app) {
  requires(appPath + "/routes").forEach(function (route) {
    route(app, config.apiPrefix);
  });
};
