angular.module("resource.messages", ["ngResource"])
.factory "Message", ['$resource',($resource) ->
  $resource "#{config.apiHost}/odata/Board:id/:action", {id:'@id',action:'@action'},
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