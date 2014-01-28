
angular.module('gallery', ['gallery-detail', 'resource.galleries']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/gallery", {
      templateUrl: "/content/app/gallery/gallery.tpl.html",
      controller: 'GalleryCtrl',
      title: 'Galleries',
      resolve: {
        galleries: [
          '$q', 'Gallery', function($q, Gallery) {
            var deferred;
            deferred = $q.defer();
            Gallery.queryOnce({
              $filter: 'IsDeleted eq false'
            }, function(data) {
              return deferred.resolve(data.value);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('GalleryCtrl', [
  "$scope", "galleries", function($scope, galleries) {
    return $scope.list = galleries;
  }
]);
