request = require('request')
mongoose = require('mongoose')
Category = mongoose.model("Category")
Article = mongoose.model("Article")

module.exports = (host) ->
  url = "#{host}/odata/Channel?$expand=Groups/Posts/Tags,Groups/Posts/Comments&$inlinecount=allpages"

  console.log "[ARTICLE] Loading data..."
  request.get {url: url, json: true},  (e, r, data) ->
    for channel in data.value when !channel.IsDeleted
      category = new Category
        name: channel.Name.replace(/[ ]/g,'-')
        description: channel.Description
        main: channel.IsDefault
      category.save()
      console.log "[CATEGORY] [#{i}] '#{channel.Name}' import is complate."

    for channel in data.value when !channel.IsDeleted
      for group in channel.Groups when !group.IsDeleted
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
          console.log "[ARTICLE] [#{i}] '#{post.Title}' import is complate."