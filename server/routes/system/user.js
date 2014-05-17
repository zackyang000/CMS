users = require('../../controllers/users');

module.exports = function (app, prefix) {
  app.get(prefix + "/users", users.all);
  app.post(prefix + "/users", users.create);
  app.get(prefix + "/users/:userId", users.get);
  app.put(prefix + "/users/:userId", users.update);
  app.del(prefix + "/users/:userId", users.destroy);

  // action
  app.post(prefix + "/users/autoLogin", users.autoLogin);
  app.post(prefix + "/users/login", users.login);
  app.post(prefix + "/users/logout", users.logout);

  // Find user by userId param before handle.
  app.param("userId", users.user);
};