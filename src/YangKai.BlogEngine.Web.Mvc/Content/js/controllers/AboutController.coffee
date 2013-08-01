AboutController=["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='关于'
  $scope.$parent.showBanner=false
  $scope.loading=true

  $http.get("/data/technology.js").success (data) ->
    $scope.list = data
    $scope.loading = false 
]