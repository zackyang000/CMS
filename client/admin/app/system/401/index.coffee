angular.module('system-401',['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/401",
          templateUrl: "/app/system/401/index.tpl.html"
          controller: ->)
  ])
