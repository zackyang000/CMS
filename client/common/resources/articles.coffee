angular.module("resource.articles", ["ngResource"])
.factory "Articles", ['$resource', ($resource) ->
  $resource "#{config.apiHost}/articles/:id/:action", {id:'@id'},
    query:
      method: "GET"
    update:
      method: "PUT"
    addComment:
      method: "POST"
      params:
        action: "comments"
]