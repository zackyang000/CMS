var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var PhotoSchema = new Schema({
  gallery: String,
  name: String,
  description: String,
  url: String,
  thumbnail: String
});

mongoose.model("Photo", PhotoSchema);