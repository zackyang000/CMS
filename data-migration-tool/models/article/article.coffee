mongoose = require("mongoose")
Schema = mongoose.Schema

ArticleSchema = new Schema
  url:
    type: String
    unique: true
  title: String
  content: String
  description: String
  category: String
  status: String
  password: String
  date: Date
  meta:
    author: String
    views: Number
    comments: Number
    tags: [ String ]
    source: String
    thumbnail: String
  comments:
    [
      author:
        name: String
        email: String
      content: String
      date: Date
      block: Boolean
    ]

mongoose.model "Article", ArticleSchema