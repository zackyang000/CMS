angular.module('gallery',['gallery-detail','resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/app/gallery/gallery.tpl.html"
      controller: 'GalleryCtrl'
      title: 'Galleries'
      resolve:
        galleries: ['$q','Gallery',($q,Gallery)->
          deferred = $q.defer()
          Gallery.queryOnce 
            $filter:'IsDeleted eq false'
          , (data) -> 
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('GalleryCtrl',
["$scope","galleries", ($scope,galleries) ->
  $scope.list=galleries
])