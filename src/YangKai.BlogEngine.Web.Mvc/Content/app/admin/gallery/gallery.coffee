angular.module('admin-gallery',['admin-gallery-edit'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/gallery",
    templateUrl: "/content/app/admin/gallery/gallery.tpl.html"
    controller: 'GalleryCtrl')
])

.controller('GalleryCtrl',
["$scope","$routeParams","$location","Gallery","Photo"
($scope,$routeParams,$location,Gallery,Photo) ->
  $scope.list = Gallery.query()
])