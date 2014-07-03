var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var CommentSchema = new Schema({
  author: String,
  content: String,
  email: String,
  date: Date,
  type: String,
  linkId: String
});

mongoose.model("Comment", CommentSchema);