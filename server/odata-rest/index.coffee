###
  app: express app object,
  url: request url,
  model: mongoose model
  options:
    maxTop: 10
    maxSkip: undefined
    defaultOrderby: 'date desc'
  actions: undefined
###

_ = require("lodash")
mongoose = require('mongoose')
create = require("./create")
update = require("./update")
read = require("./read")
del = require("./delete")

_options =
  prefix : 'oData'

exports.register = (params) ->
  app = _options.app
  url = params.url
  mongooseModel = mongoose.model(params.modelName)
  options = _.extend(_options, params.options)
  actions = params.actions || []

  app.post "/#{_options.prefix}/#{url}", (req, res, next) -> create(req, res, next, mongooseModel)
  app.put "/#{_options.prefix}/#{url}/:id", (req, res, next) -> update(req, res, next, mongooseModel)
  app.del "/#{_options.prefix}/#{url}/:id", (req, res, next) -> del(req, res, next, mongooseModel)
  app.get "/#{_options.prefix}/#{url}/:id", (req, res, next) -> read.get(req, res, next, mongooseModel)
  app.get "/#{_options.prefix}/#{url}", (req, res, next) -> read.getAll(req, res, next, mongooseModel, options)

  app.post "/#{_options.prefix}/#{url}/:id/#{item.url}", item.handle  for item in actions

exports.registerFunction = (params) ->
  url = params.url
  method = params.method
  handle = params.handle
  _options.app[method.toLowerCase()]("/#{_options.prefix}/#{url}", handle)

exports.options =
  set: (key, value) ->
    _options[key] = value