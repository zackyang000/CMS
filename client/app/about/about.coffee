angular.module('about',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/about",
      templateUrl: "/app/about/about.tpl.html"
      controller: 'AboutCtrl'
      title: 'About'
])

.controller('AboutCtrl',
["$scope", ($scope) ->

])