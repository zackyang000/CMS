var requires = require("../util/requires");
var appPath = process.cwd();
module.exports = function () {
  requires(appPath + "/models");
};
