var fs = require("fs"),
  path = require("path"),
  appPath = process.cwd();

module.exports = function (app) {
  app.get('/admin', function (req, res) {
    res.sendfile(path.join(appPath, '../client/admin-index.html'));
  });

  app.get('/admin(/*)', function (req, res) {
    res.sendfile(path.join(appPath, '../client/admin-index.html'));
  });

  app.get(/^(?!\/api\/).*/, function (req, res) {
    res.sendfile(path.join(appPath, '../client/index.html'));
  });
};
