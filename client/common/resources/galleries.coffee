angular.module("resource.galleries", ["ngResource"])
.factory "Galleries", ['$resource',($resource) ->
  $resource "#{config.apiHost}/galleries/:id/:action", {id:'@id'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc'
    queryOnce:
      cache:true
      method: "GET"
      params:
        $orderby:'CreateDate desc'
    update:
      method: "PUT"
]