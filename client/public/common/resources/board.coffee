angular.module("resource.board", ["ngResource"])
.factory "Board", ['$resource', ($resource) ->
  $resource "#{config.url.api}/board/:id/:action", {id:'@id'},
    query:
      cache: true
      method: "GET"
      params:
        $orderby: 'date desc'
    update:
      method: "PUT"
]