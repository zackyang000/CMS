GlobalController=["$scope","$http", ($scope,$http) ->
  $scope.isAdmin=false

  $http.get("/api/user").success (data) ->
    $scope.isAdmin = data.isAdmin
    $scope.Name = data.Name
    $scope.Email = data.Email
    $scope.Url = data.Url
  ]