var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var GallerySchema = new Schema({
  name: String,
  description: String,
  cover: String,
  isHidden: String,
  pubDate: Date
});

mongoose.model("Gallery", GallerySchema);