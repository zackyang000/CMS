angular.module('gallery-detail',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/gallery/:name",
    templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html"
    controller: 'GalleryDetailCtrl')
])

.controller('GalleryDetailCtrl',
["$scope","$routeParams","progressbar","Gallery",
($scope,$routeParams,progressbar,Gallery) ->
  $scope.$parent.title='Gallery '+$routeParams.name
  $scope.$parent.showBanner=false

  $scope.loading="Loading"
  $scope.name = $routeParams.name
  Gallery.get
    $filter:"Name eq '#{$scope.name}'"
    $expand:"Photos"
   , (data)->
    $scope.item=data.value[0]
    $scope.loading=""
    $('#gallery').photobox('a', { thumbs: true }###, callbackAAA###)
])