angular.module("ArticleServices", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/api/article/:id", {id:'@id'},
    querybypaged:
      method: "GET"
]