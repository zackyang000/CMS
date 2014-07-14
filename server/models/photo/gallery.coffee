mongoose = require("mongoose")
Schema = mongoose.Schema

GallerySchema = new Schema(
  name: String
  description: String
  cover: String
  isHidden: String
  date: Date
)

mongoose.model "Gallery", GallerySchema