request = require('request')
mongoose = require('mongoose')
require("./models/article/article")
Article = mongoose.model("Article")

host = "www.woshinidezhu.com"
port = 80
url = "http://#{host}:#{port}/odata/Channel?$expand=Groups/Posts/Comments&$inlinecount=allpages"


mongoose.connect "mongodb://127.0.0.1/cms-dev"


request.get {url:"./data.json", json:true},  (e, r, data) ->
  data = require(__dirname + '/data.json')
  for channel in data.value when !channel.IsDeleted
    for group in channel.Groups when !group.IsDeleted
      for post in group.Posts when !post.IsDeleted
        #article = new Article({title:"testImport."})
        #article.save() 
        for comment in post.Comments when !comment.IsDeleted
          console.log comment

