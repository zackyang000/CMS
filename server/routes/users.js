users = require('../controllers/users');

module.exports = function (app, prefix) {
  app.get(prefix + "/users", users.all);
  app.post(prefix + "/users", users.create);
  app.get(prefix + "/users/:userId", users.get);
  app.put(prefix + "/users/:userId", users.update);
  app.del(prefix + "/users/:userId", users.destroy);
  app.param(prefix + "/userId", users.user);
};