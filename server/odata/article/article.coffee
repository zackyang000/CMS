odata = require('node-odata')
auth = require('../auth')
model = require('../../models')
repository = require('../../repositories')
rss = require('../../services/rss')

rest = {}

rest.get =
  after: (entity) ->
    addViewCount(entity)

rest.getAll =
  after: (data) ->
    if data.value.length == 1
      addViewCount(data.value[0])

rest.post =
  auth: auth.admin
  after: generateRss

rest.put =
  auth: auth.admin
  after: generateRss

rest.delete =
  auth: auth.admin
  after: generateRss

addViewCount = (entity) ->
  entity.meta.views = entity.meta.views || 0
  entity.meta.views++
  entity.save()

generateRss = ->
  repository.get('article').find().sort(date: 'desc').limit(50).exec (err, data) ->
    rss.generateArticles(data)

actions = {}

# 添加评论
actions['/add-comment'] = (req, res, next) ->
  repository.get('article').findById req.params.id, (err, article) ->
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
        repository.get('comment').find().sort(date: 'desc').limit(10).exec (err, data) ->
          count = 0
          for item in data
            ((comment)->
              repository.get('article').findById item.articleId, (err, article) ->
                comment.article = article
                count++
                if count == data.length
                  rss.generateComments(data)
            )(item)


module.exports =
  url: '/articles'
  model: model.article
  options:
    defaultOrderby: 'date desc'
  rest: rest
  actions: actions
