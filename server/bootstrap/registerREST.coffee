mongoose = require('mongoose')

module.exports = (app) ->
  client = require('../odata-rest')

  client.options.set('maxTop', 10)

  client.register
    app: app,
    url: 'articles',
    model: mongoose.model("Article")
    options:
      defaultOrderby: 'date desc'
  client.register
    app: app,
    url: 'categories',
    model: mongoose.model("Category")
    options:
      defaultOrderby: 'date desc'
  client.register
    app: app,
    url: 'galleries',
    model: mongoose.model("Gallery")
    options:
      defaultOrderby: 'date desc'
  client.register
    app: app,
    url: 'board',
    model: mongoose.model("Board")
    options:
      defaultOrderby: 'date desc'
  client.register
    app: app,
    url: 'user',
    model: mongoose.model("User")