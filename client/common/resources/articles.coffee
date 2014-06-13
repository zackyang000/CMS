angular.module("resource.articles", ["ngResource"])
.factory "Articles", ['$resource',($resource) ->
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]