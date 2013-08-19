ArticleController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.setPage = (pageNo) ->
    Article.query
      $filter:'Group/IsDeleted eq false and Group/Channel/IsDeleted eq false'
      $skip:(pageNo-1)*10
    , (data)->
      $scope.list = data
  $scope.setPage 1
]