var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var UserSchema = new Schema({
  name: {
    type: String,
    unique: true
  },
  loginName: {
    type: String,
    unique: true
  },
  password: String,
  email: String,
  token: String,
  disabled: Boolean
});

mongoose.model("User", UserSchema);