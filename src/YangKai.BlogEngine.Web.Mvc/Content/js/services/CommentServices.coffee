angular.module("CommentServices", ["ngResource"])
.factory "Comment", ['$resource',($resource) ->
  $resource "/api/comment/:id", {id:'@id'},
    recent:
      method: "GET"
      params:
        action:"recent"
    add:
      method: "PUT"
    del:
      method: "POST"
      params:
        action:'delete'
    renew:
      method: "POST"
      params:
        action:'renew'
]