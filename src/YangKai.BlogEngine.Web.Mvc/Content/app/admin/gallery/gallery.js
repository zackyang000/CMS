
angular.module('admin-gallery', ['admin-gallery-edit', 'GalleryServices', 'PhotoServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery", {
      templateUrl: "/content/app/admin/gallery/gallery.tpl.html",
      controller: 'GalleryCtrl'
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "$routeParams", "$location", "Gallery", "Photo", function($scope, $routeParams, $location, Gallery, Photo) {
    return $scope.list = Gallery.query();
  }
]);
