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


# Show an gallery include photos.
exports.get = (req, res, next) ->
  Gallery.findOne
    _id: req.params.id
  , (err, gallery) ->
    if err
      next(err)
    unless gallery
      next(new Error("Failed to load gallery " + req.query.id))
    gallery = gallery.toJSON()
    res.jsonp(gallery)


# List of galleries.
exports.all = (req, res) ->
  if req.query.name
    query = name: req.query.name
  Gallery.find(query).sort("date").exec (err, galleries) ->
    if err
      res.render "error",
        status: 500
    unless req.query.$expand and req.query.$expand is 'photos'
      for item in galleries
        item.photos = undefined
    res.jsonp(galleries)


# Delete gallery.
exports.del = (req, res, next) ->

exports.addPhoto = (req, res, next) ->

exports.delPhoto = (req, res, next) ->