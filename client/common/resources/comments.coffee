angular.module("resource.comments", ["ngResource"])
.factory "Comments", ['$resource',($resource) ->
  $resource "#{config.apiHost}/comments/:id/:action", {id:'@id'},
    recent:
      method: "GET"
      params:
        action:"recent"
    del:
      method: "POST"
      params:
        action:'delete'
    renew:
      method: "POST"
      params:
        action:'renew'
    remove:
      method: "POST"
      params:
        action:'Remove'
    recover:
      method: "POST"
      params:
        action:'Recover'
]