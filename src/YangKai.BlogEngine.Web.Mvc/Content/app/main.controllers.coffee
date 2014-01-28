angular.module('main.controllers',['resource.channels','resource.users'])

.controller('GlobalController',
["$scope","$http","$location",'$window',"Channel" ,"account","$timeout" 
($scope,$http,$location,$window,Channel,account,$timeout) ->
  account.get().then (data) ->
    $scope.User=data

  Channel.query 
    $orderby:'OrderId' 
    $filter:'IsDeleted eq false'
    $expand:'Groups'
    $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
  ,(data)->
    $scope.Channels=data.value
    #TODO: 改为指令初始化nav dropdown
    $timeout((->$('[data-hover="dropdown"]').dropdownHover()),100)

  $scope.search = ->
    $location.path("/search/#{$scope.key}")

  $scope.login = ->
    $window.location.href='/admin/'
])