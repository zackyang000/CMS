angular.module('admin-board',['resource.messages'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/admin/board",
      templateUrl: "/app-admin/board/board.tpl.html"
      controller: 'BoardCtrl'
])

.controller('BoardCtrl',
["$scope","$routeParams","$location","Message", 
($scope,$routeParams,$location,Message) ->

])