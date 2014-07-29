angular.module('gallery-list',['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/app-admin/gallery/list/gallery-list.tpl.html"
      controller: 'GalleryCtrl'
])

.controller('GalleryCtrl',
["$scope", "$routeParams", "$location", "Galleries"
($scope, $routeParams, $location, Galleries) ->
  $scope.list = Galleries.query()
])