mongoose = require("mongoose")
Gallery = mongoose.model("Gallery")
crypto = require("crypto")
_ = require("lodash")


# Create an gallery.
exports.create = (req, res, next) ->
  gallery = new Gallery(req.body)
  gallery.save (err) ->
    if err
      next(err)
    res.jsonp(gallery)


# Update an gallery.
exports.update = (req, res, next) ->
  Gallery.findOne
    name: req.params.id
  , (err, gallery) ->
    if err
      next(err)
    unless gallery
      next(new Error("Failed to load gallery " + req.params.id))
    gallery = _.extend(gallery, req.body)
    gallery.save (err) ->
      if err
        next(err)
      res.jsonp(gallery)


# Show an gallery.
exports.get = (req, res) ->
  Gallery.findOne
    loginName: req.params.id
  , (err, gallery) ->
    if err
      next(err)
    unless gallery
      next(new Error("Failed to load gallery " + req.query.id))
    res.jsonp(gallery)


# List of galleries.
exports.all = (req, res) ->
  Gallery.find().sort("date").exec (err, galleries) ->
    if err
      res.render "error",
        status: 500
    res.jsonp(galleries)


# Delete gallery.
exports.del = (req, res, next) ->