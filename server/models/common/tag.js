var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var TagSchema = new Schema({
  name: String,
  url: String,
  type: String
});

mongoose.model("Tag", TagSchema);