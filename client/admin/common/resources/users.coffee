angular.module("resource.users", ["ngResource"])
.factory "Users", ['$resource', ($resource) ->
  $resource "#{config.url.api}/users/:id/:action", {id:'@id'},
    query:
      method: "GET"
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