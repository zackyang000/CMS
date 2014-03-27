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
    security.login($scope.user).then (data)->
      context.account.name = data.UserName
      context.account.email = data.Email
      context.account.avatar = data.Avatar
      $rootScope.$broadcast "loginSuccessed"
    , (error) ->
      $scope.submitting=false
      $scope.user.Password=''
      $scope.error="Username or password wrong."

  $scope.logout = ->
    $scope.submitting=true
    security.logoff().then (data) ->
      $rootScope.$broadcast "logoutSuccessed"
])