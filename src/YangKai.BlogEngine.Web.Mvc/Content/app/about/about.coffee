angular.module('about',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/about",
    templateUrl: "/Content/app/about/about-newegg.tpl.html"
    controller: 'AboutCtrl')
])

.controller('AboutCtrl',
["$scope","$translate","$http", ($scope,$translate,$http) ->
  $scope.$parent.title='About'
  $scope.$parent.showBanner=false
])