angular.module("resource.articles", ["ngResource"])
.factory "Articles", ['$resource',($resource) ->
  $resource "#{config.apiHost}/articles/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]