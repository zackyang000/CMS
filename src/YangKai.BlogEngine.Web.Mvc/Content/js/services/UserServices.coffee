angular.module("UserServices", ["ngResource"])
.factory "User", ['$resource',($resource) ->
  $resource "/odata/User:id/:action", {id:'@id',action:'@action'},
    login:
      method: "POST"
      params:
        action:'Login' 
]