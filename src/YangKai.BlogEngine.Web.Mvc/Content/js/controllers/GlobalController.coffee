GlobalController=["$scope","$http","$location",'$window', ($scope,$http,$location,$window) ->
  $http.get("/api/user").success (data) ->
    $scope.isAdmin = data.isAdmin
    $scope.Name = data.UserName
    $scope.Email = data.Email
    $scope.Url = data.Url

  $scope.search=->
    $window.location.href="/#!/search/#{$scope.key}"
]
