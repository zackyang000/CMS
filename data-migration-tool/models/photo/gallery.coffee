mongoose = require("mongoose")
Schema = mongoose.Schema

GallerySchema = new Schema(
  name: String
  description: String
  cover: String
  hidden: Boolean
  date: Date
  photos : []
)

mongoose.model "Gallery", GallerySchema