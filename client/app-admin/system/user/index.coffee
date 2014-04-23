angular.module('system-user', ['resource.users'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider.when "/system/user",
    templateUrl : "/app-admin/system/user/index.tpl.html"
    controller : 'SystemUserCtrl'
    resolve :
      users : ["$q", "User", ($q, User)->
        deferred = $q.defer()
        User.query $select : 'UserId, UserName, LoginName, Email, IsDisabled', (data)->
          deferred.resolve data.value
        deferred.promise
      ]
])

.controller('SystemUserCtrl', ["$scope", "users", "User", "$route", ($scope, users, User, $route) ->
  $scope.list = users

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    User.get id:"(guid'#{item.UserId}')", (data) ->
      $scope.entity = data
      $scope.editDialog = true

  $scope.close = ->
    $scope.editDialog = false

  $scope.save = ->
    debugger
    if $scope.entity.UserId?
      User.update(id:"(guid'#{$scope.entity.UserId}')", $scope.entity, get)
    else
      $scope.entity.UserId = UUID.generate()
      User.save($scope.entity, get)
    $scope.editDialog = false

  get = ->
    User.query $select : 'UserId, UserName, LoginName, Email, IsDisabled', (data)->
      $scope.list = data.value
])