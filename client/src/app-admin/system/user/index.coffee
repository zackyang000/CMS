angular.module('system-user', ['resource.users'])

.config(["$routeProvider", ($routeProvider) ->
    $routeProvider.when "/system/user",
      templateUrl : "/app-admin/system/user/index.tpl.html"
      controller : 'SystemUserCtrl'
      resolve :
        users : ["$q", "User", ($q, User)->
          deferred = $q.defer()
          User.query $select : 'UserName, LoginName, Email, Avatar, IsDeleted', (data)->
            deferred.resolve data.value
          deferred.promise
        ]
  ])

.controller('SystemUserCtrl', ["$scope", "users", ($scope, users) ->
      $scope.list = users
  ])