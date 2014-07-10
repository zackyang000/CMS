mongoose = require("mongoose")
Article = mongoose.model("Article")
_ = require("lodash")

# Create article.
exports.create = (req, res, next) ->
  article = new Article(req.body)
  article.save (err) ->
    if err
      next(err)
    else
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
      else
        res.jsonp(article)

# Show article.
exports.get = (req, res) ->
  Article.findOne
    url: req.params.id
  , (err, article) ->
    if err
      next(err)
    unless article
      next new Error("Failed to load article " + req.query.id)
    res.jsonp(article)

# List of articles.
exports.all = (req, res, next) ->
  Article.find().sort("-date").exec (err, article) ->
    if err
      next(err)
    else
      res.jsonp(article)