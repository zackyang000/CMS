board = require("../../controllers/board/board")

module.exports = (app, prefix) ->
  #REST
  app.get prefix + "/board", board.all
  app.post prefix + "/board", board.create
  app.put prefix + "/board/:id", board.update
