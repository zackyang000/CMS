angular.module('admin.main.controllers',['resource.users'])

.controller('GlobalController',
["$scope","$location","account","version","$localStorage"
($scope,$location,account,version,$localStorage) ->
  account.get().then (data) ->
    $scope.User=data

  version.get().then (data)->
    return if !data.length  
    $scope.newVersion=data[0]
    if $scope.newVersion.ver!=$localStorage.ver
      $scope.newVersion.showDialog=true
  $scope.versionClick = ->
    $localStorage.ver=$scope.newVersion.ver
    $scope.newVersion.showDialog=false
])

.controller('LoginController',
["$scope","$window","User",
($scope,$window,User) ->
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

  $scope.backHome = ->
    $window.location.href='/'
])