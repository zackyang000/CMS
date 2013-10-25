angular.module('gallery-detail',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/gallery/:name",
    templateUrl: "/Content/app/gallery/detail/gallery-detail.tpl.html"
    controller: 'GalleryDetailCtrl')
])

.controller('GalleryDetailCtrl',
["$scope","$routeParams","$timeout","progressbar","Gallery",
($scope,$routeParams,$timeout,progressbar,Gallery) ->
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
    $.fn.photobox('prepareDOM')
    $('#gallery').photobox('a',{history:false})
    i=0
    j=0
    $timeout(->
      for item in $("#gallery li")
        $timeout(->
          i
          $($("#gallery li")[j++]).addClass('loaded')
        25 * i++)
    1000)
])