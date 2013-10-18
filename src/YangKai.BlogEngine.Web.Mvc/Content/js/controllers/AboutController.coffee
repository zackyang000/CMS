AboutController=["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='About'
  $scope.$parent.showBanner=false

  $scope.loading="Loading"
  $http.get("/Content/data/technology.js").success (data) ->
    $scope.list = data
    $scope.loading=""
]