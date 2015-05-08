fs = require("fs")
appPath = process.cwd()
repository = require './../../repositories'

initData = (model, path) ->
  require(path).forEach (item) ->
    data = new model(item)
    data.save()
    console.log "data init: #{path} import successful."

module.exports = ->
  repository.get('user').find().exec (err, users) ->
    unless users.length
      initData(repository.get('user'), "./system/user.json")
      initData(repository.get('article'), "./article/article.json")
      initData(repository.get('category'), "./article/category.json")
      initData(repository.get('board'), "./board/board.json")
      initData(repository.get('gallery'), "./photo/gallery.json")
