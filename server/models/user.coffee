mongoose = require('mongoose')
Schema = mongoose.Schema

UserSchema = new Schema
  id : String
  name :
    type : String
    unique : true
  loginName :
    type : String
    unique : true
  password : String
  email : String
  avatar : String
  token : String
  inUser : String
  inDate : Date
  editUser : String
  editDate : Date
  isDeleted : Boolean
  remark : String

mongoose.model "User", UserSchema