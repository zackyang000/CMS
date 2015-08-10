mongoose = require("mongoose")
Schema = mongoose.Schema

CategorySchema = new Schema(
  name: String
  description: String
  main: Boolean
)

mongoose.model "Category", CategorySchema