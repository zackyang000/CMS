mongoose = require("mongoose")
Schema = mongoose.Schema

TagSchema = new Schema(
  name: String
  collections: {}
)

mongoose.model "Tag", TagSchema