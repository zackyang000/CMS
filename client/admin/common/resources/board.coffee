angular.module("resource.board", ["ngResource"])
.factory "Board", ['$resource', ($resource) ->
  $resource "#{config.apiHost}/board/:id/:action", {id:'@id'},
    query:
      method: "GET"
      params:
        $orderby: 'date desc'
    update:
      method: "PUT"
]