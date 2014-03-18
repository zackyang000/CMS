angular.module('main.controllers',['resource.channels','resource.users',"ChannelServices"])

.controller('GlobalController',
["$scope","$http","$location",'$window',"Channel" ,"account","$timeout","channel"
($scope,$http,$location,$window,Channel,account,$timeout,channel) ->
  account.get().then (data) ->
    $scope.User=data

  $scope.$on "ChannelChange",(event, channel) ->
    $scope.channelUrl = channel.Url

  $scope.config = config
])

.controller('TopController',
["$scope","$http","$location",'$window',"Channel" ,"account","$timeout","channel"
($scope,$http,$location,$window,Channel,account,$timeout,channel) ->
  $scope.login = ->
    $window.location.href='/admin/'
])

.controller('HeaderController',
["$scope","$http","$location",'$window',"Channel" ,"account","$timeout","channel"
($scope,$http,$location,$window,Channel,account,$timeout,channel) ->
  channel.get().then (data) ->
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