Resource = require('node-odata').Resource
model = require('../../models')
auth = require('../auth')

module.exports = Resource('comment', model.comment)
.orderBy('date desc')
.put()
  .auth(auth.admin)
.delete()
  .auth(auth.admin)
