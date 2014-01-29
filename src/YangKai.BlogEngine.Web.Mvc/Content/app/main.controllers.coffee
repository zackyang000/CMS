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
])