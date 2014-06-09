categories = require('../../controllers/article/categories');

module.exports = function (app, prefix) {
  app.get(prefix + "/categories", categories.all);
  app.post(prefix + "/categories", categories.create);
  app.get(prefix + "/categories/:id", categories.get);
  app.put(prefix + "/categories/:id", categories.update);
  app.del(prefix + "/categories/:id", categories.update);
};