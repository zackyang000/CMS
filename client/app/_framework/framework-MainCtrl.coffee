angular.module("framework.controllers.main",['resource.categories'])

.controller('MainCtrl',
  ["$scope", "$rootScope","$http","$location",'$window' ,"context","security"
    ($scope, $rootScope,$http,$location,$window,context,security) ->

      $scope.$on "ChannelChange",(event, channel) ->
        $scope.channelUrl = channel

      $rootScope.config = config
  ])
