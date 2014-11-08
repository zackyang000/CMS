angular.module('gallery-detail',['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/:name",
      templateUrl: "/app/gallery/detail/gallery-detail.tpl.html"
      controller: 'GalleryDetailCtrl'
      resolve:
        gallery: ['$route', '$q', 'Galleries', ($route, $q, Galleries) ->
          deferred = $q.defer()
          Galleries.query
            $filter: "name eq '#{$route.current.params.name}'"
          , (data) ->
            deferred.resolve data.value[0]
          deferred.promise
        ]
])

.controller('GalleryDetailCtrl',
["$scope","$rootScope","$translate","gallery","$timeout"
($scope,$rootScope,$translate,gallery,$timeout) ->
  $rootScope.title = 'Gallery ' + gallery.name
  $scope.gallery = gallery
])
