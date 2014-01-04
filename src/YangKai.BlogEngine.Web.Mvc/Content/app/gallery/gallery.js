
angular.module('gallery', ['gallery-detail', 'resource.galleries']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery", {
      templateUrl: "/content/app/gallery/gallery.tpl.html",
      controller: 'GalleryCtrl'
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "$translate", "$routeParams", "$location", "Gallery", function($scope, $translate, $routeParams, $location, Gallery) {
    $scope.$parent.title = 'Galleries';
    $scope.$parent.showBanner = false;
    $scope.loading = $translate("global.loading");
    return Gallery.query(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
]);
