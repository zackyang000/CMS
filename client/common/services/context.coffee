angular.module("zy.services.context", [])
.factory "context", ['$http','$localStorage',($http,$localStorage) ->
  _account= undefined

  obj = {}
  Object.defineProperty obj, "account",
    get: ->
      _account = $localStorage.account || {name: undefined, email: undefined, avatar: '/img/avatar.png'} unless _account
      return _account
    set: (val) ->
      $localStorage.account = val
      _account = val

  obj.auth = admin : false

  return obj
]