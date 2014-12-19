odata = require('node-odata')
auth = require('../auth')
model = require('../../models')
repository = require('../../repositories')
rss = require('../../services/rss')

rest = {}

rest.post =
  auth: auth.admin
  after: (req, res) ->
    Article = repository.get('article')
    Article.find().sort(date: 'desc').limit(50).exec (err, data) ->
      rss.generateArticles(data)

rest.put =
  auth: auth.admin

rest.delete =
  auth: auth.admin


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

# 增加浏览量
actions['/browsed'] = (req, res, next) ->
  Article = repository.get('article')
  Article.findOne
    _id: req.params.id
  , (err, article) ->
    return next(err)  if err
    return next new Error('Failed to load article ' + req.query.id)  unless article

    article.meta.views = article.meta.views || 0
    article.meta.views++
    article.save (err) ->
      if err
        next(err)
      res.send(200)


module.exports =
  url: '/articles'
  model: model.article
  options:
    defaultOrderby: 'date desc'
  rest: rest
  actions: actions
