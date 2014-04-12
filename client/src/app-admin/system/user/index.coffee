angular.module('system-user',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/system/user",
      templateUrl: "/app-admin/system/user/index.tpl.html"
      controller: 'SystemUserCtrl'
])

.controller('SystemUserCtrl',
["$scope","$http"
($scope,$http) ->

])