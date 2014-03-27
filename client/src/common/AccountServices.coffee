angular.module("zy.services.context", [])
.factory "context", ['$http','$localStorage',($http,$localStorage) ->
  account: $localStorage.account || {name: 'Guest', email: undefined, avatar: '/img/avatar.png'}
]