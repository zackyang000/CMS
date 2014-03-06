angular.module('main.controllers',['resource.channels','resource.users',"ChannelServices"])

.controller('GlobalController',
["$scope","$http","$location",'$window',"Channel" ,"account","$timeout","channel"
($scope,$http,$location,$window,Channel,account,$timeout,channel) ->
  account.get().then (data) ->
    $scope.User=data

  channel.get().then (data) ->
    $scope.Channels=data

    #TODO: 改为指令初始化nav dropdown
    #$timeout((->$('[data-hover="dropdown"]').dropdownHover()),100)

  $scope.search = ->
    $location.path("/search/#{$scope.key}")

  $scope.login = ->
    $window.location.href='/admin/'
    
  $scope.isActive = (route) ->  
    route == $location.path()
    
  $scope.isActiveChannel = (channel) ->
    #首页
    return true if channel.IsDefault && ($location.path() == "/" || $location.path() == "/list")
    #article list
    return true if $location.path().indexOf(channel.Url) > -1
    #article detail
    return $location.path().indexOf("/post") > -1 && channel.Url.indexOf($scope.channelUrl) > -1
    
  $scope.$on("ChannelChange",(event, channel) ->
    $scope.channelUrl = channel.Url
  )
])