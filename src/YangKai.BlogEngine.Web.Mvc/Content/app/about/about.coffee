angular.module('about',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/about",
    templateUrl: "/Content/app/about/about-newegg.tpl.html"
    controller: 'AboutCtrl')
])

.controller('AboutCtrl',
["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='About'
  $scope.$parent.showBanner=false

  $scope.loading="Loading"
  $http.get("/Content/data/technology.js").success (data) ->
    $scope.list = data
    $scope.loading=""
])