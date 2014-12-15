angular.module('system-user', ['resource.users', 'user-edit'])

.config(['$routeProvider', ($routeProvider) ->
  $routeProvider.when '/system/user',
    templateUrl : '/app/system/user/user.tpl.html'
    controller : 'SystemUserCtrl'
    resolve :
      users : ["$q", "Users", ($q, Users)->
        deferred = $q.defer()
        Users.query (data) ->
          deferred.resolve data.value
        deferred.promise
      ]
])

.controller('SystemUserCtrl',
['$scope', 'users', 'Users', 'ngDialog',
($scope, users, Users, ngDialog) ->
  $scope.list = users

  load = ->
    Users.query (data) ->
      $scope.list = data.value

  $scope.openAddDialog = ()->
    $scope.entity = {}
    openDialog()

  $scope.openEditDialog = (item) ->
    $scope.entity = item
    openDialog()

  openDialog = ->
    dialog = ngDialog.open
      template: '/app/system/user/user-edit-dialog.tpl.html'
      controller: 'UserEditDialogCtrl'
      scope: $scope
    dialog.closePromise.then load
])
