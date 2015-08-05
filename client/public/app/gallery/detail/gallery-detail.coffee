angular.module('gallery-detail',['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/:id",
      templateUrl: "/app/gallery/detail/gallery-detail.tpl.html"
      controller: 'GalleryDetailCtrl'
      resolve:
        gallery: ['$route', '$q', 'Galleries', ($route, $q, Galleries) ->
          deferred = $q.defer()
          Galleries.list
            $filter: "url eq '#{$route.current.params.id}'"
          , (data) ->
            deferred.resolve data.value[0]
          deferred.promise
        ]
])

.controller('GalleryDetailCtrl',
["$scope", '$rootScope', '$translate', "gallery", 'context'
($scope, $rootScope, $translate, gallery, context) ->
  $rootScope.title = gallery.name[context.language]
  $scope.gallery = gallery
])
