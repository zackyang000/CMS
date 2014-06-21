angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource', '$q', '$injector',($resource, $q, $injector) ->
  User = $resource('/user/:userId', {userId:'@id'})
  debugger
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    update:
      method: "PUT"
    default:
      (->
        deferred = $q.defer()
        return $injector.get('Categories').query(  (data) ->
          debugger
          deferred.resolve data
        deferred.promise
        )
      )()
]
