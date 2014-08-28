angular.module("resource.board", ["ngResource"])
.factory "Board", ['$resource', ($resource) ->
  $resource "#{config.apiHost}/board/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]