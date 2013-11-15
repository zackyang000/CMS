angular.module('admin-board',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/admin/board",
    templateUrl: "/content/app/admin/board/board.tpl.html"
    controller: 'BoardCtrl')
])

.controller('BoardCtrl',
["$scope","$routeParams","$location","Article", 
($scope,$routeParams,$location,Article) ->

])