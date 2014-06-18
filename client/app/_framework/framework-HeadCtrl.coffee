angular.module("framework.controllers.head",['resource.categories'])

.controller('HeaderCtrl',
  ["$scope","$http","$location",'$window',"Categories" ,"$timeout"
    ($scope,$http,$location,$window,Categories,$timeout) ->
      Categories.query (data) ->
        $scope.Channels=data

      $scope.isActiveChannel = (channel) ->
        #home page
        return true if channel.IsDefault && ($location.path() == "/" || $location.path() == "/list")
        #article list
        return true if $location.path().indexOf(channel.Url) > -1
        #article detail
        return $location.path().indexOf("/post") > -1 && channel.Url.indexOf($scope.channelUrl) > -1

      $scope.isActive = (route) ->
        route == $location.path()

      $scope.search = ->
        $location.path("/search/#{$scope.key}")
  ])
