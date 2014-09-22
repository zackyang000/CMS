create = require("./create")
update = require("./update")
read = require("./read")
del = require("./delete")

exports.register = (options) ->
  app = options.app
  url = options.url
  mongooseModel = options.model
  meta = options.meta || {}

  prefix = 'oData'

  app.post "/#{prefix}/#{url}", (req, res, next) -> create(req, res, next, mongooseModel)
  app.put "/#{prefix}/#{url}/:id", (req, res, next) -> update(req, res, next, mongooseModel)
  app.del "/#{prefix}/#{url}/:id", (req, res, next) -> del(req, res, next, mongooseModel)
  app.get "/#{prefix}/#{url}/:id", (req, res, next) -> read.get(req, res, next, mongooseModel)
  app.get "/#{prefix}/#{url}", (req, res, next) -> read.getAll(req, res, next, mongooseModel, meta)