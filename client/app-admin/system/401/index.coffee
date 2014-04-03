angular.module('admin-system-401',['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/401",
          templateUrl: "/app-admin/system/401/index.tpl.html"
          controller: ->)
  ])
