angular.module("AccountServices", [])
.factory "account", ['$http','$rootScope',($http,$rootScope) ->
  $http.get("/admin/getuser").success (data) ->
    if data.Email
      data.Gravatar='http://www.gravatar.com/avatar/' + md5(data.Email) 
    else
      data.Gravatar='/Content/img/avatar.png'
    $rootScope.User=data
]