angular.module("framework.controllers.login",['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/login",
          template: ""
          controller: ->)
  ])

.controller('LoginCtrl',["$scope", "$rootScope", "security"
($scope,$rootScope,security) ->
  $scope.login = ->
    $scope.submitting=true
    $scope.error=''
    security.login($scope.user)
    .then (data)->
      $rootScope.$broadcast "loginSuccessed"
    , (error) ->
      $scope.submitting=false
      $scope.user.Password=''
      $scope.error=error

  $scope.logout = ->
    $scope.submitting=true
    security.logoff().then (data) ->
      $rootScope.$broadcast "logoutSuccessed"
])