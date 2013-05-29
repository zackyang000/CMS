var GlobalController;

GlobalController = [
  "$scope", "$http", function($scope, $http) {
    $scope.loading = false;
    $scope.isAdmin = false;
    return $http.get("/comment/UserInfo").success(function(data) {
      $scope.isAdmin = data.isAdmin;
      $scope.Name = data.Name;
      $scope.Email = data.Email;
      return $scope.Url = data.Url;
    });
  }
];
