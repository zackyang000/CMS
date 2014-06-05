var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var UserSchema = new Schema({
  _id: mongoose.Schema.Types.ObjectId,
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