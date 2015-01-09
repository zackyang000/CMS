mongoose = require('node-odata').mongoose

module.exports =
  get: (name) ->
    if name is 'article'
      return mongoose.model('articles')
    if name is 'comment'
      return mongoose.model('comments')
    if name is 'category'
      return mongoose.model('categories')
    if name is 'board'
      return mongoose.model('board')
    if name is 'gallery'
      return mongoose.model('galleries')
    if name is 'user'
      return mongoose.model('users')
