angular.module('gallery-detail',['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/:name",
      templateUrl: "/app/gallery/detail/gallery-detail.tpl.html"
      controller: 'GalleryDetailCtrl'
      resolve:
        gallery: ['$route', '$q', 'Galleries', ($route, $q, Galleries) ->
          deferred = $q.defer()
          Galleries.get
            id: $route.current.params.name
          , (data) ->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('GalleryDetailCtrl',
["$scope","$rootScope","$translate","gallery","$timeout"
($scope,$rootScope,$translate,gallery,$timeout) ->
  $rootScope.title = 'Gallery '+ gallery.Name
  $scope.gallery = gallery

  #TODO:改为指令加载相册
  $.fn.photobox('prepareDOM')
  $('.gallery').photobox('a',{history:false})
  i=0
  j=0
  $timeout(->
    for item in $(".gallery li")
      $timeout(->
        i
        $($(".gallery li")[j++]).addClass('loaded')
      25 * i++)
  500)
])