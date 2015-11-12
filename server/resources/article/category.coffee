Resource = require('node-odata').Resource
model = require('../../models')
auth = require('../auth')

module.exports = Resource('category', model.category)
.orderBy('date desc')
.post()
  .auth(auth.admin)
.put()
  .auth(auth.admin)
.delete()
  .auth(auth.admin)
