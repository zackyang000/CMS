angular.module('system-404',['ngRoute'])

.config(["$routeProvider",
    ($routeProvider) ->
      $routeProvider
      .when("/404",
          templateUrl: "/app/system/404/index.tpl.html"
          controller: ->)
  ])