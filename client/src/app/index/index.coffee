angular.module('index',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/index",
      templateUrl: "/app/index/index.tpl.html"
      controller: 'IndexCtrl'
      title: 'Home'
])

.controller('IndexCtrl',
["$scope","$http", ($scope,$http) ->
  $http.get("/data/words.js").success (data) ->
    $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1)-1]
    Index.initLayerSlider();
])