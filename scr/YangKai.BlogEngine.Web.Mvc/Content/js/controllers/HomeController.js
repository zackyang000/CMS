var HomeController;

HomeController = [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = '首页';
    $scope.$parent.showBanner = true;
    return $http.get("/data/words.js").success(function(data) {
      $scope.list = data;
      debugger;
      return $scope.$parent.word = $scope.list[0];
    });
  }
];
