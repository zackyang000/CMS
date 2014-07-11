mongoose = require("mongoose")
Comment = mongoose.model("Comment")
_ = require("lodash")


# Create comment.
exports.create = (req, res, next) ->
  comment = new Comment(req.body)
  comment.save (err) ->
    if err
      next(err)
    res.jsonp(comment)


# Update comment.
exports.update = (req, res, next) ->
  Comment.findOne
    _id: req.params.id
  , (err, comment) ->
    if err
      next(err)
    unless comment
      next(new Error("Failed to load comment #{req.params.id}"))
    comment = _.extend(comment, req.body)
    comment.save (err) ->
      if err
        next(err)
      res.jsonp(comment)


# Show comment.
exports.get = (req, res, next) ->
  params = req.params.id.split("/")
  query = type: params[0]
  if params.length is 2
    query.linkId = params[1]

  Comment.find query, (err, comment) ->
    if err
      next(err)
    unless comment
      next(new Error("Failed to load comment #{req.query.id}"))
    res.jsonp comment


# List of comments.
exports.all = (req, res) ->
  Comment.find().sort("date").exec (err, comment) ->
    if err
      res.render "error",
        status: 500
    res.jsonp(comment)