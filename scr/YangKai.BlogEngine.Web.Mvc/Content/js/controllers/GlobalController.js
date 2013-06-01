var GlobalController;

GlobalController = [
  "$scope", "$http", function($scope, $http) {
    $scope.isAdmin = false;
    return $http.get("/api/user").success(function(data) {
      $scope.isAdmin = data.isAdmin;
      $scope.Name = data.Name;
      $scope.Email = data.Email;
      return $scope.Url = data.Url;
    });
  }
];
