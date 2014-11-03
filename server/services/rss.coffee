fs = require('fs')
path = require('path')
RSS = require('rss')


exports.generateArticles = (list) ->
  feed = new RSS
    title: "iShare - Articles"
    description: "iShare - Articles"
    feed_url: "http://feed.woshinidezhu.com/articles.xml"
    site_url: "http://www.woshinidezhu.com"
    image_url: "http://www.woshinidezhu.com/img/favicon.ico"
    managingEditor: "Zack Yang"
    webMaster: "Zack Yang"
    copyright: "2014 Zack Yang"
    language: "cn"
    pubDate: new Date()
    ttl: "60"

  for item in list
    feed.item
      title: item.title
      description: item.content
      url: "http://www.woshinidezhu.com/post/#{item.url}"
      guid: item._id
      author: item.meta.author
      date: item.date

  xml = feed.xml()

  fs.writeFile(path.join(path.dirname(__dirname), 'static/articles.xml'), xml)


exports.generateComments = (list) ->
  feed = new RSS
    title: "iShare - Comments"
    description: "iShare - Comments"
    feed_url: "http://feed.woshinidezhu.com/comments.xml"
    site_url: "http://www.woshinidezhu.com"
    image_url: "http://www.woshinidezhu.com/img/favicon.ico"
    managingEditor: "Zack Yang"
    webMaster: "Zack Yang"
    copyright: "2014 Zack Yang"
    language: "cn"
    pubDate: new Date()
    ttl: "60"

  for item in list
    feed.item
      title: item.article.title
      description: item.content
      url: "http://www.woshinidezhu.com/post/#{item.article.url}"
      guid: item._id
      author: item.author.name
      date: item.date

  xml = feed.xml()

  fs.writeFile(path.join(path.dirname(__dirname), 'static/comments.xml'), xml)