angular.module('gallery',['gallery-detail','resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/content/app/gallery/gallery.tpl.html"
      controller: 'GalleryCtrl'
      title: 'Galleries'
])

.controller('GalleryCtrl',
["$scope","$translate","$routeParams","$location","Gallery"
($scope,$translate,$routeParams,$location,Gallery) ->
  $scope.loading=$translate("global.loading")
  Gallery.query (data) ->
    $scope.list=data
    $scope.loading=""
])