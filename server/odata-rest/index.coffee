_ = require("lodash")


exports.register = (app, url, model) ->
  prefix = 'oData'

  #create
  app.post "/#{prefix}/#{url}", (req, res, next) ->
    entity = new model(req.body)
    entity.save (err) ->
      next(err)  if err
      res.jsonp(entity)

  #update
  app.put "/#{prefix}/#{url}/:id", (req, res, next) ->
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
  app.del "/#{prefix}/#{url}/:id", (req, res, next) ->
    model.remove
      _id: req.params.id
    , (err) ->
      next(err)  if err
      res.send(204)

  #read
  app.get "/#{prefix}/#{url}/:id", (req, res, next) ->
    model.findOne
      _id: req.params.id
    , (err, entity) ->
      next(err)  if err
      next(new Error("Failed to find #{url} [#{req.params.id}]"))  unless article
      res.jsonp(entity)

  #read(odata)
  app.get "/#{prefix}/#{url}", (req, res, next) ->
    params = odataParser(req.query)

    resData = {}

    query = model.find()

    if params.$filter
      1 #todo: query.where



    # ?$count=true
    # ->
    # @odata.count: 8
    if params.$count
      if params.$count == 'true'
        model.find().count (err, count) ->
          resData['@odata.count'] = count
      else
        next(new Error('Unknown $count option, only "true" and "false" are supported.'))


    # ?$orderby=ReleaseDate asc, Rating desc
    # ->
    # query.sort({ ReleaseDate: 'asc', Rating: 'desc' });
    if params.$orderby
      orderbyArr = params.$orderby.split(',')
      sort = {}
      for orderby in orderbyArr
        data = orderby.trim().split(' ')
        if data.length != 2
          next(new Error("odata: Failed to parser $orderby '#{query.$orderby}', it's should be like 'ReleaseDate asc, Rating desc'"))
        sort[data[0].trim()] = data[1]
      query.sort(sort)


    # ?$top=10
    # ->
    # query.limit(10)
    if params.$top
      count = +params.$top
      if count == count && count > 0
        query.limit(count)
      else
        next(new Error("Incorrect format for $top argument '#{query.$top}'."))

    # ?$skip=10
    # ->
    # query.skip(10)
    if params.$skip
      count = +params.$skip
      if count == count && count > 0
        query.skip(count)
      else
        next(new Error("Incorrect format for $skip argument '#{params.$skip}'."))

    # $expand=Customers/Orders
    query.exec (err, data) ->
      resData.value = data
      res.jsonp resData


odataParser = (params) ->
  $filter : params['$filter']
  $orderby : params['$orderby']
  $count : params['$count']
  $top : params['$top']
  $skip : params['$skip']
  $expand : params['$expand']
  $select : params['$select']

    #todo:
###
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