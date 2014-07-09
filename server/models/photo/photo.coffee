mongoose = require("mongoose")
Schema = mongoose.Schema

PhotoSchema = new Schema(
  gallery: String
  name: String
  description: String
  url: String
  thumbnail: String
)

mongoose.model "Photo", PhotoSchema