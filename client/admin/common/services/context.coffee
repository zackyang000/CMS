angular.module("zy.services.context", [])
.factory "context", ['$http','$localStorage',($http,$localStorage) ->
  _account = undefined

  obj = {}
  Object.defineProperty obj, "account",
    get: ->
      _account || $localStorage.account || {name: undefined, email: undefined, avatar: '/img/avatar.png'}
    set: (val) ->
      _account = val
      $localStorage.account = val

  obj.auth =
    admin : false

  return obj
]