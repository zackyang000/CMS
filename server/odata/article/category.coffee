odata = require('node-odata')
model = require('../../models')
auth = require('../auth')


rest = {}

rest.post =
  auth: auth.admin

rest.put =
  auth: auth.admin

rest.delete =
  auth: auth.admin

module.exports =
  url: 'categories'
  model: model.category
  options:
    defaultOrderby: 'date desc'
  rest: rest
