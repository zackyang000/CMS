comments = require("../../controllers/common/comments")

module.exports = (app, prefix) ->
  #REST
  app.get prefix + "/comments", comments.all
  app.post prefix + "/comments", comments.create
  app.get prefix + "/comments/:id", comments.get
  app.put prefix + "/comments/:id", comments.update
