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
  date: Date,
  meta:{
    views: Number,
    comments: Number,
    tags: [],
    source: String,
    thumbnail: String
  }
});

mongoose.model("Article", ArticleSchema);