categories = require("../../controllers/article/categories")
module.exports = (app, prefix) ->
  #ACTION
  app.get prefix + "/categories/default", categories.main

  #REST
  app.get prefix + "/categories", categories.all
  app.post prefix + "/categories", categories.create
  app.get prefix + "/categories/:id", categories.get
  app.put prefix + "/categories/:id", categories.update