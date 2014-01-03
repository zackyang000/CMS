angular.module("AccountServices", [])
.factory "account", ['$http','$q',($http,$q) ->
  get: ->
    deferred = $q.defer()
    if @data
      deferred.resolve @data
    else
      self=this
      $http.get("/admin/getuser")
        .success (data) ->
          self.data=data
          deferred.resolve self.data
        .error (data) ->
          deferred.reject data
    deferred.promise
]