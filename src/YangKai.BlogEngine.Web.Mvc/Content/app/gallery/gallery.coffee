angular.module('gallery',
['gallery-detail','GalleryServices','PhotoServices'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/gallery",
    templateUrl: "/content/app/gallery/gallery.tpl.html"
    controller: 'GalleryCtrl')
])

.controller('GalleryCtrl',
["$scope","$routeParams","$location","Gallery","Photo"
($scope,$routeParams,$location,Gallery,Photo) ->
  $scope.$parent.title='Galleries'
  $scope.$parent.showBanner=false

  $scope.loading="Loading"
  Gallery.query (data) ->
    $scope.list=data
    $scope.loading=""
])