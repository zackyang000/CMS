var GlobalController;

GlobalController = [
  "$scope", "$http", "$location", function($scope, $http, $location) {
    $scope.isAdmin = false;
    $http.get("/api/user").success(function(data) {
      $scope.isAdmin = data.isAdmin;
      $scope.Name = data.UserName;
      $scope.Email = data.Email;
      return $scope.Url = data.Url;
    });
    $scope.search = function() {
      return $location.path("/search/" + $scope.key);
    };
    return $scope.breadcrumbchange = function(data) {
      $scope.breadcrumb = data;
      debugger;
    };
  }
];
