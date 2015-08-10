mongoose = require("mongoose")
Schema = mongoose.Schema

UserSchema = new Schema(
  name:
    type: String
    unique: true
  loginName:
    type: String
    unique: true
  password: String
  email: String
  token: String
  disabled: Boolean
)

mongoose.model("User", UserSchema)