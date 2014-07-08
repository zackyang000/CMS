angular.module("resource.photos", ["ngResource"])
.factory "Photos", ['$resource',($resource) ->
  $resource "#{config.apiHost}/photos/:id/:action", {id:'@id'},
    update:
      method: "PUT"
]