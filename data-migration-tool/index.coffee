request = require('request')
mongoose = require('mongoose')
require("./models/article/article")
Article = mongoose.model("Article")

host = "http://www.woshinidezhu.com:80"
host = "http://192.168.192.174:8088"
url = "#{host}/odata/Channel?$expand=Groups/Posts/Comments,Groups/Posts/Tags&$inlinecount=allpages"

mongoose.connect "mongodb://127.0.0.1/cms-dev"

request.get {url:"./data.json", json:true},  (e, r, data) ->
  data = require(__dirname + '/data.json')
  for channel in data.value when !channel.IsDeleted
    for group in channel.Groups when !group.IsDeleted
      for post in group.Posts when !post.IsDeleted
        tags = (item.Name for item in post.Tags)
        article = new Article
          url: post.Url
          title: post.Title
          content: post.Content
          description: post.Description
          category: channel.Name
          status: "normal"
          password: undefined
          date: post.PubDate
          meta:
            author: post.CreateUser
            views: post.ViewCount
            comments: post.Comments.length
            tags: [ group.Name ].concat(tags)
            source:
            thumbnail:
          comments:
          [
            author:
              name:
              email:
            content:
            date:
            block:
          ]
          
        #article.save()
        for comment in post.Comments when !comment.IsDeleted
          console.log comment