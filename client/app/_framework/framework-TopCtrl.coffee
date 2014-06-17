angular.module("framework.controllers.top", [])

.controller('TopCtrl',
  ["$scope","$http","$location",'$window',"Channel" ,"$timeout","channel"
    ($scope,$http,$location,$window,Channel,$timeout,channel) ->
      $scope.login = ->
        $window.location.href='/admin/'
  ])
