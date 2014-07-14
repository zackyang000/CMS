mongoose = require("mongoose")
Schema = mongoose.Schema

ArticleSchema = new Schema(
  title: String
  url:
    type: String
    unique: true
  content: String
  description: String
  category: String
  status: String
  password: String
  date:
    type: Date
    default: Date.now
  meta:
    author: String
    views: Number
    comments: Number
    tags: []
    source: String
    thumbnail: String
)

mongoose.model "Article", ArticleSchema