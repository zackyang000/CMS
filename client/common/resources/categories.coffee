angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource',($resource) ->
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    query:
      method: "GET"
    queryOnce:
      cache:true
      method: "GET"
    edit:
      method: "PUT"
]