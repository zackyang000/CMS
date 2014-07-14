angular.module('gallery',['gallery-detail','resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/app/gallery/gallery.tpl.html"
      controller: 'GalleryCtrl'
      title: 'Galleries'
      resolve:
        galleries: ['$q','Galleries',($q,Galleries)->
          deferred = $q.defer()
          Galleries.query  (data) ->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('GalleryCtrl',
["$scope","galleries", ($scope,galleries) ->
  $scope.galleries = galleries
])