angular.module("resource.messages", ["ngResource"])
.factory "Messages", ['$resource',($resource) ->
  $resource "#{config.apiHost}/boards/:id/:action", {id:'@id'},
    query:
      method:"GET"
      params:
        $orderby:"CreateDate desc"
    queryOnce:
      cache:true
      method:"GET"
      params:
        $orderby:"CreateDate desc"
    remove:
      method: "POST"
      params:
        action:'Remove'
    recover:
      method: "POST"
      params:
        action:'Recover'
]