angular.module('about',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/about",
      templateUrl: "/Content/app/about/about.tpl.html"
      controller: 'AboutCtrl'
      title: 'About'
])

.controller('AboutCtrl',
["$scope","$translate","$http", 
($scope,$translate,$http) ->
])