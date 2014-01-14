angular.module('gallery',['gallery-detail','resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/content/app/gallery/gallery.tpl.html"
      controller: 'GalleryCtrl'
])

.controller('GalleryCtrl',
["$scope","$translate","$routeParams","$location","Gallery"
($scope,$translate,$routeParams,$location,Gallery) ->
  $scope.$parent.title='Galleries'
  $scope.$parent.showBanner=false

  $scope.loading=$translate("global.loading")
  Gallery.query (data) ->
    $scope.list=data
    $scope.loading=""
])