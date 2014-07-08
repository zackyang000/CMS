angular.module('board',['resource.comments'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/board",
      templateUrl: "/app-admin/board/board.tpl.html"
      controller: 'BoardCtrl'
])

.controller('BoardCtrl',
["$scope","$routeParams","$location","Message", 
($scope,$routeParams,$location,Message) ->

])