users = require('../../controllers/users');

module.exports = function (app, prefix) {
  app.get(prefix + "/users", users.all);
  app.post(prefix + "/users", users.create);
  app.get(prefix + "/users/:userId", users.get);
  app.put(prefix + "/users/:userId", users.update);
  app.del(prefix + "/users/:userId", users.destroy);

  // action
  app.post(prefix + "/users/autoSignin", users.autoSignin);
  app.post(prefix + "/users/signin", users.signin);
  app.post(prefix + "/users/signout", users.signout);

  // Find user by userId param before handle.
  app.param("userId", users.user);
};