var fs = require("fs");

module.exports = function (path) {
  modules = []
  var requireAll = function (path) {
    fs.readdirSync(path).forEach(function (file) {
      var newPath = path + "/" + file;
      var stat = fs.statSync(newPath);
      if (stat.isFile()) {
        if (/(.*)\.(js$)/.test(file)) {
          modules.push(require(newPath));
        }
      }
      else if (stat.isDirectory()) {
        requireAll(newPath);
      }
    });
  };
  requireAll(path);
  return modules;
};
