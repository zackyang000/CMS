mongoose = require("mongoose")
Schema = mongoose.Schema

GallerySchema = new Schema(
  name: String
  description: String
  cover: String
  hidden: String
  date: Date
  photos: Array
)

mongoose.model "Gallery", GallerySchema