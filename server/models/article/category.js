var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var CategorySchema = new Schema({
  name: String,
  url: {
    type: String,
    unique: true
  },
  description: String,
  default: Boolean
});

mongoose.model("Category", CategorySchema);