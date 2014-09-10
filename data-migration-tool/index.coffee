request = require('request')
mongoose = require('mongoose')
require("./bootstrap/registerModels")()

Article = mongoose.model("Article")

host = "http://www.woshinidezhu.com:80"
host = "http://192.168.192.174:8088"
url = "#{host}/odata/Channel?$expand=Groups/Posts/Tags,Groups/Posts/Comments&$inlinecount=allpages"

mongoose.connect "mongodb://127.0.0.1/cms-dev"

request.get {url: url, json: true},  (e, r, data) ->
  for channel in data.value when !channel.IsDeleted
    console.log "[CHANNEL] Begin import '#{channel.Name}'."
    for group in channel.Groups when !group.IsDeleted
      console.log "[GROUP] Begin import '#{group.Name}'."
      for post, i in group.Posts when !post.IsDeleted
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
            source: post.Source
            thumbnail: post.Thumbnail
          comments: []
        for comment in post.Comments
          article.comments.push
            author:
              name: comment.Author
              email: comment.Email
            content: comment.Content
            date: comment.CreateDate
            block: comment.IsDeleted
        article.save()
        console.log "[ARTICLE] [#{i}] Import '#{post.Title}' is complate."