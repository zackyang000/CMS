angular.module('admin-gallery-list',['resource.galleries'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/admin/gallery",
    templateUrl: "/content/app/admin/gallery/list/gallery-list.tpl.html"
    controller: 'GalleryCtrl')
])

.controller('GalleryCtrl',
["$scope","$routeParams","$location","Gallery"
($scope,$routeParams,$location,Gallery) ->
  $scope.list = Gallery.query()
])