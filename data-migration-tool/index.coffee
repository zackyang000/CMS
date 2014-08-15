request = require('request')

host = "www.woshinidezhu.com"
port = 80
url = "http://#{host}:#{port}/odata/Channel?$expand=Groups/Posts/Comments&$filter=IsDeleted+eq+false&$inlinecount=allpages&$orderby=Url&$select=Name,Url,Groups%2FName,Groups%2FUrl,Groups%2FIsDeleted,Groups%2FPosts%2FTitle,Groups%2FPosts%2FUrl,Groups%2FPosts%2FPubDate,Groups%2FPosts%2FIsDeleted"

request.get {url:url, json:true},  (e, r, data) ->
  debugger
  console.log(data)
