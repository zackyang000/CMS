angular.module("resource.users", ["ngResource"])
.factory "User", ['$resource',($resource) ->
  $resource "/odata/User:id/:action", {id:'@id',action:'@action'},
    signin:
      method: "POST"
      params:
        action:'Signin' 
    signout:
      method: "POST"
      params:
        action:'Signout' 
]