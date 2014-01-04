angular.module('ctrl.main',['resource.channels','resource.users'])

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

  $scope.GoHome = ->
    $window.location.href='/'
])

.controller('LoginController',
["$scope","$window","User",
($scope,$window,User) ->
  $scope.open = ->
    $window.location.href='/admin/'

  $scope.signin = ->
    $scope.submitting=true
    User.signin {id:'(1)'},$scope.user
      ,(data)->
        $scope.submitting=false
        $window.location.href='/admin/'
      ,(error)->
        $scope.error=error.data['odata.error'].innererror.message
        $scope.user.Password=''
        $scope.submitting=false

  $scope.signout = ->
    $scope.submitting=true
    User.signout {id:'(1)'}
      ,(data)->
        $scope.submitting=false
        $window.location.href='/'

  $scope.manage = ->
    $window.location.href='/admin/'

  $scope.view = ->
    $window.location.href='/'
])