
angular.module('gallery-detail', ['resource.galleries']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery/:name", {
      templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html",
      controller: 'GalleryDetailCtrl',
      resolve: {
        gallery: [
          '$route', '$q', 'Gallery', function($route, $q, Gallery) {
            var deferred;
            deferred = $q.defer();
            Gallery.queryOnce({
              $filter: "Name eq '" + $route.current.params.name + "' and IsDeleted eq false",
              $expand: "Photos"
            }, function(data) {
              return deferred.resolve(data.value[0]);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('GalleryDetailCtrl', [
  "$scope", "$rootScope", "$translate", "gallery", "$timeout", function($scope, $rootScope, $translate, gallery, $timeout) {
    var i, j;
    $rootScope.title = 'Gallery ' + gallery.Name;
    $scope.item = gallery;
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
  }
]);
