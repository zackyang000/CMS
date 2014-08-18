mongoose = require("mongoose")
Schema = mongoose.Schema

CommentSchema = new Schema(
  author: String
  content: String
  email: String
  date:
    type: Date
    default: Date.now

  type: String
  block: Boolean
  linkId: String
)

mongoose.model "Comment", CommentSchema