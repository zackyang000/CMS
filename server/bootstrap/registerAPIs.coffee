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
    #todo:
    #odataParser
    #req.params.$top
