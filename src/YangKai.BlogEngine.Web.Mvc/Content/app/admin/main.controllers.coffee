angular.module('admin.main.controllers',['resource.users'])

.controller('GlobalController',
["$scope","$location","account","$localStorage"
($scope,$location,account,$localStorage) ->
  account.get().then (data) ->
    $scope.User=data
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