users = require('../../controllers/users');

module.exports = function (app, prefix) {
  app.get(prefix + "/users", users.all);
  app.post(prefix + "/users", users.create);
  app.get(prefix + "/users/:id", users.get);
  app.put(prefix + "/users/:id", users.update);

  // action
  app.post(prefix + "/users/autoLogin", users.autoLogin);
  app.post(prefix + "/users/login", users.login);
  app.post(prefix + "/users/logout", users.logout);
};