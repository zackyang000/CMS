
angular.module('gallery-detail', ['resource.galleries']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery/:name", {
      templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html",
      controller: 'GalleryDetailCtrl'
    });
  }
]).controller('GalleryDetailCtrl', [
  "$scope", "$translate", "$routeParams", "$timeout", "progressbar", "Gallery", function($scope, $translate, $routeParams, $timeout, progressbar, Gallery) {
    $scope.$parent.title = 'Gallery ' + $routeParams.name;
    $scope.$parent.showBanner = false;
    $scope.loading = $translate("global.loading");
    $scope.name = $routeParams.name;
    return Gallery.get({
      $filter: "Name eq '" + $scope.name + "'",
      $expand: "Photos"
    }, function(data) {
      var i, j;
      $scope.item = data.value[0];
      $scope.loading = "";
      $.fn.photobox('prepareDOM');
      $('.gallery').photobox('a', {
        history: false
      });
      i = 0;
      j = 0;
      return $timeout(function() {
        var item, _i, _len, _ref, _results;
        _ref = $(".gallery li");
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          _results.push($timeout(function() {
            i;
            return $($(".gallery li")[j++]).addClass('loaded');
          }, 25 * i++));
        }
        return _results;
      }, 500);
    });
  }
]);
