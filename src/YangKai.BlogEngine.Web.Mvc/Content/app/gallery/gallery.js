
angular.module('gallery', ['gallery-detail', 'GalleryServices', 'PhotoServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery", {
      templateUrl: "/content/app/gallery/gallery.tpl.html",
      controller: 'GalleryCtrl'
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "$translate", "$routeParams", "$location", "Gallery", "Photo", function($scope, $translate, $routeParams, $location, Gallery, Photo) {
    $scope.$parent.title = 'Galleries';
    $scope.$parent.showBanner = false;
    $scope.loading = $translate("global.loading");
    return Gallery.query(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
]);
