var AboutController;

AboutController = [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = 'About';
    $scope.$parent.showBanner = false;
    $scope.loading = "Loading";
    return $http.get("/Content/data/technology.js").success(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
];
