mongoose = require("mongoose")
Article = mongoose.model("Article")
_ = require("lodash")


# Create article.
exports.create = (req, res, next) ->
  article = new Article(req.body)
  article.save (err) ->
    if err
      next(err)
    res.jsonp(article)


# Update article.
exports.update = (req, res, next) ->
  Article.findOne
    _id: req.params.id
  , (err, article) ->
    if err
      next(err)
    unless article
      next(new Error("Failed to load article " + req.params.id))
    article = _.extend(article, req.body)
    article.save (err) ->
      if err
        next(err)
      res.jsonp(article)


# Show article.
exports.get = (req, res, next) ->
  Article.findOne
    _id: req.params.id
  , (err, article) ->
    if err
      next(err)
    unless article
      next new Error("Failed to load article " + req.query.id)
    res.jsonp(article)


# List of articles.
exports.all = (req, res, next) ->
  if req.query.url
    query = url: req.query.url
  Article.find(query).sort("-date").limit(10).skip(10).exec (err, article) ->
    if err
      next(err)
    res.jsonp(article)


# Create comment.
exports.createComment = (req, res, next) ->
  Article.findOne
    _id: req.params.id
  , (err, article) ->
    if err
      next(err)
    unless article
      next new Error("Failed to load article " + req.query.id)
    req.body.date = new Date()
    article.comments.push(req.body)
    article.save (err) ->
      if err
        next(err)
      res.jsonp(req.body)