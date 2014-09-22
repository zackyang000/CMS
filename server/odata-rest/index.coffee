###
  app: express app object,
  url: request url,
  model: mongoose model
  meta:
    maxTop: 10
    maxSkip: undefined
    defaultOrderby: 'date desc'
    action: undefined
    function: undefined
###

_ = require("lodash")
create = require("./create")
update = require("./update")
read = require("./read")
del = require("./delete")

defaultOptions = {}

exports.register = (params) ->
  app = params.app
  url = params.url
  mongooseModel = params.model
  options = _.extend(defaultOptions, params.options)

  prefix = 'oData'

  app.post "/#{prefix}/#{url}", (req, res, next) -> create(req, res, next, mongooseModel)
  app.put "/#{prefix}/#{url}/:id", (req, res, next) -> update(req, res, next, mongooseModel)
  app.del "/#{prefix}/#{url}/:id", (req, res, next) -> del(req, res, next, mongooseModel)
  app.get "/#{prefix}/#{url}/:id", (req, res, next) -> read.get(req, res, next, mongooseModel)
  app.get "/#{prefix}/#{url}", (req, res, next) -> read.getAll(req, res, next, mongooseModel, options)

exports.options =
  set: (key, value) ->
    defaultOptions[key] = value
