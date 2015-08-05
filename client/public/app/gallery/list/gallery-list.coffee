angular.module('gallery',['gallery-detail','resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery",
      templateUrl: "/app/gallery/list/gallery-list.tpl.html"
      controller: 'GalleryCtrl'
      title: 'Galleries'
      resolve:
        galleries: ['$q','Galleries',($q,Galleries)->
          deferred = $q.defer()
          Galleries.list
            $top: 1000
            $select: 'url, name, description, cover, hidden'
            $orderby: 'order'
            $filter: 'hidden eq false'
          ,(data) ->
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('GalleryCtrl',
["$scope", "galleries", 'context', ($scope, galleries, context) ->
  group = []
  current = undefined
  for item, i in galleries
    if i % 3 == 0
      current = []
      group.push current
    current.push item

  $scope.group = group
])
