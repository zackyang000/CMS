angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource',($resource) ->
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]