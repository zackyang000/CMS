Resource = require('node-odata').Resource
model = require('../../models')
auth = require('../auth')
rss = require('../../services/rss')
resources = require('node-odata').resources

module.exports = Resource('article', model.article)
.orderBy('date desc')
.list()
  .after (data) ->
    if data.value.length == 1
      entity = data.value[0]
      entity.meta.views = entity.meta.views || 0
      entity.meta.views++
      entity.save()
.post()
  .auth(auth.admin)
  .after(rssGenerate)
.put()
  .auth(auth.admin)
  .after(rssGenerate)
.delete()
  .auth(auth.admin)
  .after(rssGenerate)
.action '/add-comment', (req, res, next) ->
  resources.article.findById req.params.id, (err, article) ->
    return next(err)  if err
    return next(new Error('Failed to load article ' + req.query.id))  unless article
    article.comments.push(req.body)
    article.meta.comments = article.comments.length
    article.save (err) ->
      return next(err)  if err
      res.jsonp(req.body)
      comment = new Comment(req.body)
      comment.articleId = req.params.id
      comment.save ->
        resources.comment.find().sort(date: 'desc').limit(10).exec (err, data) ->
          count = 0
          for item in data
            ((comment)->
              resouces.article.findById item.articleId, (err, article) ->
                comment.article = article
                count++
                if count == data.length
                  rss.generateComments(data)
            )(item)


rssGenerate = ->
  resources.article.find().sort(date: 'desc').limit(50).exec (err, data) ->
    rss.generateArticles(data)
