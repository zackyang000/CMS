angular.module('gallery-detail',['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/:name",
      templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html"
      controller: 'GalleryDetailCtrl'
])

.controller('GalleryDetailCtrl',
["$scope","$rootScope","$translate","$routeParams","$timeout","progressbar","Gallery",
($scope,$rootScope,$translate,$routeParams,$timeout,progressbar,Gallery) ->
  $rootScope.title='Gallery '+$routeParams.name
  $scope.$parent.showBanner=false

  $scope.loading=$translate("global.loading")
  $scope.name = $routeParams.name
  Gallery.get
    $filter:"Name eq '#{$scope.name}'"
    $expand:"Photos"
   , (data)->
    $scope.item=data.value[0]
    $scope.loading=""
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