mongoose = require("mongoose")
Board = mongoose.model("Board")
_ = require("lodash")


# Create message.
exports.create = (req, res, next) ->
  board = new Board(req.body)
  board.save (err) ->
    if err
      next(err)
    res.jsonp(board)


# Update message.
exports.update = (req, res, next) ->
  Board.findOne
    _id: req.params.id
  , (err, board) ->
    if err
      next(err)
    unless board
      next(new Error("Failed to load board #{req.params.id}"))
    board = _.extend(board, req.body)
    board.save (err) ->
      if err
        next(err)
      res.jsonp(board)


# List of messages.
exports.all = (req, res) ->
  Board.find().sort("date").exec (err, board) ->
    if err
      res.render "error",
        status: 500
    res.jsonp(board)