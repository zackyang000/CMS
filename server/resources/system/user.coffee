Resource = require('node-odata').Resource
model = require('../../models')
auth = require('../auth')
resources = require('node-odata').resources

module.exports = Resource('user', model.user)
.orderBy('date desc')
.list()
  .auth(auth.admin)
.get()
  .auth(auth.admin)
.post()
  .auth(auth.admin)
.delete()
  .auth(auth.admin)
.put()
  .auth(auth.admin)
  .after (newEntity, oldEntity) ->
    resources.article.find( {'meta.author': oldEntity.name }).exec (err, articles) ->
      for article in articles
        article.meta.author = newEntity.name
        article.save()

