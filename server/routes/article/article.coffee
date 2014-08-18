articles = require("../../controllers/article/articles")

module.exports = (app, prefix) ->
  #REST
  app.get prefix + "/articles", articles.all
  app.post prefix + "/articles", articles.create
  app.get prefix + "/articles/:id", articles.get
  app.put prefix + "/articles/:id", articles.update
  app.del prefix + "/articles/:id", articles.update

  app.post prefix + "/articles/:id/comments", articles.createComment
