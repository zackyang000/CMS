mongoose = require('mongoose')

module.exports = (app) ->
  client = require('../odata-rest')

  client.register
    app: app,
    url: 'articles',
    model: mongoose.model("Article")
    meta:
      maxTop: 10
      maxSkip: undefined
      defaultOrderby: 'date desc'
      action: undefined         #todo Not implement
      function: undefined       #todo Not implement
  client.register
    app: app,
    url: 'categories',
    model: mongoose.model("Category")
  client.register
    app: app,
    url: 'galleries',
    model: mongoose.model("Gallery")
  client.register
    app: app,
    url: 'board',
    model: mongoose.model("Board")
  client.register
    app: app,
    url: 'user',
    model: mongoose.model("User")