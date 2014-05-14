var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var ArticleSchema = new Schema({
  title: String,
  url: {
    type: String,
    unique: true
  },
  content: String,
  description: String,
  category: String,
  subCategory: String,
  status: String,
  password: String,
  pubDate: Date,
  viewCount: String,
  commentCount: String,
  tags: [],
  source: String,
  thumbnail: String
});

mongoose.model("Article", ArticleSchema);