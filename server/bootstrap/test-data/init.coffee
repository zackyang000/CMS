fs = require("fs")
appPath = process.cwd()
repository = require './../../repositories'

initData = (model, path) ->
  require(appPath + path).forEach (item) ->
    data = new model(item)
    data.save()
    console.log "data init: #{path} import successful."

module.exports = ->
  repository.get('user').find().exec (err, users) ->
    unless users.length
      initData(repository.get('user'), "/bootstrap/test-data/system/user.json")
      initData(repository.get('article'), "/bootstrap/test-data/article/article.json")
      initData(repository.get('category'), "/bootstrap/test-data/article/category.json")
      initData(repository.get('board'), "/bootstrap/test-data/board/board.json")
      #initData(repository.get('tag'), "/bootstrap/test-data/common/tag.json")
      initData(repository.get('gallery'), "/bootstrap/test-data/photo/gallery.json")
