ArticleDetailController=["$scope","$routeParams","Article", ($scope,$routeParams,Article) ->
  $scope.$parent.showBanner=false
  $scope.$parent.loading=true

  $scope.url =$routeParams.url
  $scope.item = Article.get
    id:$scope.url
  , ->
    $scope.$parent.loading=false
]