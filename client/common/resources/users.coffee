angular.module("resource.users", ["ngResource"])
.factory "Users", ['$resource',($resource) ->
  $resource "#{config.apiHost}/users/:id/:action", {id:'@id'},
    update:
      method: "PUT"
    autoSignin:
      method: "POST"
      params:
        action:'auto-login'
    signin:
      method: "POST"
      params:
        action:'login'
    signout:
      method: "POST"
      params:
        action:'logout'
]