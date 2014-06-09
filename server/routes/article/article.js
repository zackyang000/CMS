articles = require('../../controllers/article/articles');

module.exports = function (app, prefix) {
  app.get(prefix + "/articles", articles.all);
  app.post(prefix + "/articles", articles.create);
  app.get(prefix + "/articles/:id", articles.get);
  app.put(prefix + "/articles/:id", articles.update);
  app.del(prefix + "/articles/:id", articles.update);
};