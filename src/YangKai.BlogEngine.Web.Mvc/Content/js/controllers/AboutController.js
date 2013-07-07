var AboutController;

AboutController = [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = '关于';
    $scope.$parent.showBanner = false;
    $scope.loading = true;
    return $http.get("/data/technology.js").success(function(data) {
      $scope.list = data;
      return $scope.loading = false;
    });
  }
];
