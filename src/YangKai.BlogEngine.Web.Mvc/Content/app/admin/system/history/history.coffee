angular.module('admin-system-history',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/admin/system/history",
    templateUrl: "/content/app/admin/system/history/history.tpl.html"
    controller: 'SystemHistoryCtrl')
])

.controller('SystemHistoryCtrl',
["$scope","$http",'VersionService'
($scope,$http,VersionService) ->
  VersionService.get().then (data)->
    $scope.versions=data
])