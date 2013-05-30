var AboutController;

AboutController = [
  "$scope", "$http", function($scope, Message, $http) {
    $scope.$parent.showBanner = false;
    $scope.$parent.loading = true;
    return $http.get("/data/technology.js").success(function(data) {
      $scope.list = data;
      return $scope.$parent.loading = false;
    });
  }
];
