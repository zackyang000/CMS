angular.module("framework.controllers.login",['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/login",
          template: ""
          controller: ->)
  ])

.controller('LoginCtrl',["$scope", "$rootScope", "security", "context"
($scope,$rootScope,security, context) ->
  $scope.login = ->
    $scope.submitting=true
    $scope.error=''
    security.login($scope.user).then (data)->
      context.account=
        name:data.UserName
        email:data.Email
        avatar:data.Avatar
      context.auth.admin = true
      $rootScope.$broadcast "loginSuccessed"
    , (error) ->
      $scope.submitting=false
      $scope.user.Password=''
      $scope.error="Username or password wrong."

  $scope.logout = ->
    context.auth.admin = false
    security.logoff().then (data) ->
      $rootScope.$broadcast "logoutSuccessed"
])