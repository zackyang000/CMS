var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var GallerySchema = new Schema({
  name: String,
  description: String,
  cover: String,
  isHidden: String,
  date: Date
});

mongoose.model("Gallery", GallerySchema);