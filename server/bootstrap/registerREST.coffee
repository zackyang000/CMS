mongoose = require('mongoose')

module.exports = (app) ->
  client = require('../odata-rest')

  client.register(app, 'articles', mongoose.model("Article"))