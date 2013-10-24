
angular.module('gallery-detail', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery/:name", {
      templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html",
      controller: 'GalleryDetailCtrl'
    });
  }
]).controller('GalleryDetailCtrl', [
  "$scope", "$routeParams", "progressbar", "Gallery", function($scope, $routeParams, progressbar, Gallery) {
    $scope.$parent.title = 'Gallery ' + $routeParams.name;
    $scope.$parent.showBanner = false;
    $scope.loading = "Loading";
    $scope.name = $routeParams.name;
    return Gallery.get({
      $filter: "Name eq '" + $scope.name + "'",
      $expand: "Photos"
    }, function(data) {
      $scope.item = data.value[0];
      $scope.loading = "";
      return $('#gallery').photobox('a', {
        thumbs: true
      });
    });
  }
]);
