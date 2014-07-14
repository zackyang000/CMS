angular.module("resource.comments", ["ngResource"])
.factory "Comments", ['$resource',($resource) ->
  $resource "#{config.apiHost}/comments/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]