GlobalController=["$scope","$http", ($scope,$http) ->
  $scope.loading=false
  $scope.isAdmin=false
  $http.get("/comment/UserInfo").success (data) ->
    $scope.isAdmin = data.isAdmin
    $scope.Name = data.Name
    $scope.Email = data.Email
    $scope.Url = data.Url
  ]