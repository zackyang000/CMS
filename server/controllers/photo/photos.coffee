mongoose = require("mongoose")
Photo = mongoose.model("Photo")


# Create an photo.
exports.create = (req, res, next) ->
  photo = new Photo(req.body)
  photo.save (err) ->
    if err
      next(err)
    res.jsonp(photo)


# Delete photo.
exports.del = (req, res, next) ->