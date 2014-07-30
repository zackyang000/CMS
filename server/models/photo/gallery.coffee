mongoose = require("mongoose")
Schema = mongoose.Schema

GallerySchema = new Schema(
  name: String
  description: String
  cover: String
  hidden: Boolean
  date: Date
  photos :
    name: String
    description: String
    url: String
    thumbnail: String
)

mongoose.model "Gallery", GallerySchema