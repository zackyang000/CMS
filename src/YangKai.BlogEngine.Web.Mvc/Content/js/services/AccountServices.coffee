angular.module("AccountServices", [])
.factory "account", ['$http','$rootScope',($http,$rootScope) ->
  $http.get("/admin/getuser").success (data) ->
    $rootScope.User=data
]