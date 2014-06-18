angular.module("framework.controllers.top", [])

.controller('TopCtrl',
  ["$scope","$http","$location",'$window' ,"$timeout"
    ($scope,$http,$location,$window,$timeout) ->
      $scope.login = ->
        $window.location.href='/admin/'
  ])
