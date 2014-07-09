mongoose = require("mongoose")
Schema = mongoose.Schema

CategorySchema = new Schema(
  name:
    type: String
    unique: true

  description: String
  main: Boolean
)

mongoose.model "Category", CategorySchema