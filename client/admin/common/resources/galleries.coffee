angular.module("resource.galleries", ["ngResource"])
.factory "Galleries", ['$resource', ($resource) ->
  $resource "#{config.url.api}/galleries/:id/:action", {id:'@id'},
    query:
      method: "GET"
      params:
        $orderby: 'date desc'
    update:
      method: "PUT"
]