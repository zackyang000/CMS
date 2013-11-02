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
["$scope","$translate","$routeParams","$location","Gallery","Photo"
($scope,$translate,$routeParams,$location,Gallery,Photo) ->
  $scope.$parent.title='Galleries'
  $scope.$parent.showBanner=false

  $scope.loading=$translate("global.loading")
  Gallery.query (data) ->
    $scope.list=data
    $scope.loading=""
])