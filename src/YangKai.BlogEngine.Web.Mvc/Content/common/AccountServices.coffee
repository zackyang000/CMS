angular.module("AccountServices", [])
.factory "account", ['$http','$q',($http,$q) ->
  get: ->
    deferred = $q.defer()
    $http.get("/admin/getuser",cache:true)
      .success (data) ->
        deferred.resolve data
      .error (data) ->
        deferred.reject data
    deferred.promise
]