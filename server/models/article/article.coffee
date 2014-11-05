module.exports =
  url: String
  title: String
  content: String
  markdown: String
  description: String
  category: String
  status: String
  password: String
  editor: String
  date: Date
  meta:
    author: String
    views: Number
    comments: Number
    tags: [ String ]
    source: String
    thumbnail: String
  comments:[
    author:
      name: String
      email: String
    content: String
    date: Date
  ]