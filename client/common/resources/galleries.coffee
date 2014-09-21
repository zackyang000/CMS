angular.module("resource.galleries", ["ngResource"])
.factory "Galleries", ['$resource', ($resource) ->
  $resource "#{config.apiHost}/galleries/:id/:action", {id:'@id'},
    query:
      method: "GET"
    update:
      method: "PUT"
]