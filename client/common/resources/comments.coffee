angular.module("resource.comments", ["ngResource"])
.factory "Comments", ['$resource',($resource) ->
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]