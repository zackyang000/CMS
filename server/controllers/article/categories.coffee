mongoose = require("mongoose")
Category = mongoose.model("Category")
_ = require("lodash")

# Create category.
exports.create = (req, res, next) ->
  category = new Category(req.body)
  category.save (err) ->
    if err
      next(err)
    res.jsonp(category)

# Update category.
exports.update = (req, res, next) ->
  Category.findOne
    _id: req.params.id
  , (err, category) ->
    if err
      next(err)
    unless category
      next(new Error("Failed to load category " + req.params.id))
    category = _.extend(category, req.body)
    category.save (err) ->
      if err
        next(err)
      res.jsonp(category)


# Get default category.
exports.main = (req, res, next) ->
  Category.findOne
    main: true
  , (err, category) ->
    if err
      next err
    unless category
      next new Error("Failed to load category.")
    res.jsonp(category)


# Show category.
exports.get = (req, res, next) ->
  Category.findOne
    _id: req.params.id
  , (err, category) ->
    if err
      next err
    unless category
      next new Error("Failed to load category " + req.query.id)
    res.jsonp(category)

# List of categories.
exports.all = (req, res) ->
  Category.find().sort("name").exec (err, category) ->
    if err
      res.render "error",
        status: 500
    res.jsonp(category)
