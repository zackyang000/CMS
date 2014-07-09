var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var CategorySchema = new Schema({
  name: {
    type: String,
    unique: true
  },
  description: String,
  main: Boolean
});

mongoose.model("Category", CategorySchema);