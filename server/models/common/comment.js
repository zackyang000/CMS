var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var CommentSchema = new Schema({
  author: String,
  content: String,
  email: String,
  url: String,
  pubDate: Date,
  type: String
});

mongoose.model("Comment", CommentSchema);