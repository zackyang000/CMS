angular.module('dashboard',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/",
      templateUrl: "/app/dashboard/dashboard.tpl.html"
      controller: 'DashboardCtrl'
])

.controller('DashboardCtrl',
["$scope","$http",
($scope,$http) ->

])