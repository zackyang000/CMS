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
          Galleries.query
            $top: 10000
            $select: '_id, name, description, cover, hidden'
          ,(data) ->
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('GalleryCtrl',
["$scope", "galleries", 'context', ($scope, galleries, context) ->
  $scope.galleries = galleries
  $scope.context = context
])
