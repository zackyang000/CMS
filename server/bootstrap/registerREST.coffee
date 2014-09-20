mongoose = require('mongoose')

module.exports = (app) ->
  client = require('../odata-rest')

  client.register(app, 'articles', mongoose.model("Article"))
  client.register(app, 'categories', mongoose.model("Category"))
  client.register(app, 'galleries', mongoose.model("Gallery"))
  client.register(app, 'board', mongoose.model("Board"))
  client.register(app, 'user', mongoose.model("User"))
