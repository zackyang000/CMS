module.exports = (req, res, next, mongooseModel) ->
  query = new mongooseModel(req.body)
  query.save (err) ->
    next(err)  if err
    res.jsonp(entity)