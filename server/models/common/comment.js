var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var CommentSchema = new Schema({
  author: String,
  content: String,
  email: String,
  date: {
    type: Date,
    default: Date.now
  },
  type: String,
  block: Boolean,
  linkId: String
});

mongoose.model("Comment", CommentSchema);