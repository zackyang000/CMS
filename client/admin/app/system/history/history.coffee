angular.module('system-history',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/system/history",
      templateUrl: "/app/system/history/history.tpl.html"
      controller: 'SystemHistoryCtrl'
])

.controller('SystemHistoryCtrl',
["$scope","$http",'version'
($scope,$http,version) ->
  version.get().then (data)->
    $scope.versions=data
])