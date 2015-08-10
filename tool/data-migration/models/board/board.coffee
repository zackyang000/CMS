mongoose = require("mongoose")
Schema = mongoose.Schema

BoardSchema = new Schema(
  author:
    name: String
    email: String
  content: String
  date:
    type: Date
    default: Date.now
  block: Boolean
)

mongoose.model "Board", BoardSchema