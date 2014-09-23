angular.module('system-user', ['resource.users'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider.when "/system/user",
    templateUrl : "/app-admin/system/user/index.tpl.html"
    controller : 'SystemUserCtrl'
    resolve :
      users : ["$q", "Users", ($q, Users)->
        deferred = $q.defer()
        Users.query (data) ->
          deferred.resolve data.value
        deferred.promise
      ]
])

.controller('SystemUserCtrl', ["$scope", "users", "Users", "$route", ($scope, users, Users, $route) ->
  $scope.list = users

  $scope.isNew = false

  $scope.add = ()->
    $scope.entity = {}
    $scope.isNew = true
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy item
    $scope.isNew = false
    $scope.editDialog = true

  $scope.close = ->
    $scope.editDialog = false

  $scope.save = ->
    if $scope.isNew
      Users.save($scope.entity, get)
    else
      Users.update({id: $scope.entity.loginName}, $scope.entity, get)
    $scope.editDialog = false

  get = ->
    Users.query (data)->
      $scope.list = data
])