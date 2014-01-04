
angular.module('admin-gallery-list', ['resource.galleries']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/admin/gallery", {
      templateUrl: "/content/app/admin/gallery/list/gallery-list.tpl.html",
      controller: 'GalleryCtrl'
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "$routeParams", "$location", "Gallery", function($scope, $routeParams, $location, Gallery) {
    return $scope.list = Gallery.query();
  }
]);
