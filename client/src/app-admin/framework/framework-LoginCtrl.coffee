angular.module("framework.controller.login",['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/login",
          template: ""
          controller: ->)
  ])

.controller('LoginCtrl',["$scope","$rootScope","authorize","userProfile","$q"
($scope,$rootScope,authorize, userProfile, $q) ->
  $scope.signin = ->
    $scope.submitting=true
    $scope.error=''
    authorize.login($scope.user)
    .then (data)->
      $rootScope.$broadcast "loginSuccessed"
    , (error) ->
      $scope.submitting=false
      $scope.user.Password=''
      $scope.error=error

  $scope.signout = ->
    $scope.submitting=true
    authorize.logoff().then (data) ->
      $rootScope.$broadcast "logoutSuccessed"
])