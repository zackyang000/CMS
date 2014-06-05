angular.module('system-user', ['resource.users'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider.when "/system/user",
    templateUrl : "/app-admin/system/user/index.tpl.html"
    controller : 'SystemUserCtrl'
    resolve :
      users : ["$q", "User", ($q, User)->
        deferred = $q.defer()
        User.query (data)->
          deferred.resolve data
        deferred.promise
      ]
])

.controller('SystemUserCtrl', ["$scope", "users", "User", "$route", ($scope, users, User, $route) ->
  $scope.list = users

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy item
    $scope.editDialog = true

  $scope.close = ->
    $scope.editDialog = false

  $scope.save = ->
    if $scope.entity._id
      User.update($scope.entity, get)
    else
      $scope.entity._id = UUID.generate()
      User.save($scope.entity, get)
    $scope.editDialog = false

  get = ->
    User.query (data)->
      $scope.list = data
])