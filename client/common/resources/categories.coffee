angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource', '$q', '$injector',($resource, $q, $injector) ->
  User = $resource('/user/:userId', {userId:'@id'})
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    update:
      method: "PUT"
    default:
      method: "GET"
      params:
        action:'default'
]
