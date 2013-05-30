AboutController=["$scope","$http", ($scope,Message,$http) ->
  $scope.$parent.showBanner=false
  $scope.$parent.loading=true
  $http.get("/data/technology.js").success (data) ->
    $scope.list = data
    $scope.$parent.loading = false 
]