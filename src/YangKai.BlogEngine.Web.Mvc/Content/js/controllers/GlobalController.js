var GlobalController;

GlobalController = [
  "$scope", "$http", "$location", '$window', function($scope, $http, $location, $window) {
    $http.get("/api/user").success(function(data) {
      $scope.isAdmin = data.isAdmin;
      $scope.Name = data.UserName;
      $scope.Email = data.Email;
      return $scope.Url = data.Url;
    });
    return $scope.search = function() {
      return $window.location.href = "/#!/search/" + $scope.key;
    };
  }
];
