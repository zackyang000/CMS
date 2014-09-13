config = require("../config/config")
mongoose = require('mongoose')
_ = require("lodash")

module.exports = (app) ->
  createREST(app, 'article', mongoose.model("Article"))


createREST = (app, url, model) ->
  prefix = config.oDataPrefix

  #create
  app.post "#{prefix}/#{url}", (req, res, next) ->
    entity = new model(req.body)
    entity.save (err) ->
      next(err)  if err
      res.jsonp(entity)

  #update
  app.put "#{prefix}/#{url}/:id", (req, res, next) ->
    model.findOne
      _id: req.params.id
    , (err, entity) ->
      next(err)  if err
      next(new Error("Failed to find #{url} [#{req.params.id}]"))  unless entity
      entity = _.extend(entity, req.body)
      entity.save (err) ->
        next(err)  if err
        res.jsonp(entity)

  #delete
  app.del "#{prefix}/#{url}/:id", (req, res, next) ->
    model.remove
      _id: req.params.id
    , (err) ->
      next(err)  if err
      res.send(204)

  #read
  app.get "#{prefix}/#{url}/:id", (req, res, next) ->
    model.findOne
      _id: req.params.id
    , (err, entity) ->
      next(err)  if err
      next(new Error("Failed to find #{url} [#{req.params.id}]"))  unless article
      res.jsonp(entity)

  #read(odata)
  app.get "#{prefix}/#{url}", (req, res, next) ->
    query = odataParser(req.params)



odataParser = (params) ->
    $top = params['$top']

    #todo:
    #odataParser
    #req.params.$top $skip $orderby  $expand  $inlineCount $count
###
$filter
eq
Equal
Address/City eq 'Redmond'
ne
Not equal
Address/City ne 'London'
gt
Greater than
Price gt 20
ge
Greater than or equal
Price ge 10
lt
Less than
Price lt 20
le
Less than or equal
Price le 100
and
Logical and
Price le 200 and Price gt 3.5
or
Logical or
Price le 3.5 or Price gt 200
not
Logical negation
not endswith(Description,'milk')
###