angular.module('main.controllers',['resource.channels','resource.users'])

.controller('GlobalController',
["$scope","$http","$location",'$window',"Channel" ,"account"
($scope,$http,$location,$window,Channel,account) ->
  account.get().then (data) ->
    $scope.User=data

  Channel.query 
    $orderby:'OrderId' 
    $filter:'IsDeleted eq false'
    $expand:'Groups'
    $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
  ,(data)->
    $scope.Channels=data.value

  $scope.search = ->
    $location.path("/search/#{$scope.key}")

  $scope.login = ->
    $window.location.href='/admin/'
])