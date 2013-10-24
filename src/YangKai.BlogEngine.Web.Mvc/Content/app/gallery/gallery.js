
angular.module('gallery', ['gallery-detail', 'GalleryServices', 'PhotoServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery", {
      templateUrl: "/content/app/gallery/gallery.tpl.html",
      controller: 'GalleryCtrl'
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "$routeParams", "$location", "Gallery", "Photo", function($scope, $routeParams, $location, Gallery, Photo) {
    $scope.$parent.title = 'Galleries';
    $scope.$parent.showBanner = false;
    $scope.loading = "Loading";
    return Gallery.query(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
]);
