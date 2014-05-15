var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var TagSchema = new Schema({
  name: String,
  collections: {}
});

mongoose.model("Tag", TagSchema);