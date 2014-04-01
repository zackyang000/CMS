angular.module("resource.users", ["ngResource"])
.factory "User", ['$resource',($resource) ->
  $resource "#{config.apiHost}/odata/User:id/:action", {id:'@id',action:'@action'},
    autoSignin:
      method: "POST"
      params:
        action:'AutoSignin'
    signin:
      method: "POST"
      params:
        action:'Signin' 
    signout:
      method: "POST"
      params:
        action:'Signout' 
]