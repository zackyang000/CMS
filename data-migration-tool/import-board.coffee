request = require('request')
mongoose = require('mongoose')
Board = mongoose.model("Board")

module.exports = (host, db) ->
  url = "#{host}/odata/Board?$orderby=CreateDate+desc"
  mongoose.connect db

  request.get {url: url, json: true},  (e, r, data) ->

    for item, i in data.value
      board = new Board
        author:
          name: item.Author
          email: item.Email
        content: item.Content
        date: item.CreateDate
        block: item.IsDeleted
      board.save()
      console.log "[BOARD] [#{i}] import is complate."