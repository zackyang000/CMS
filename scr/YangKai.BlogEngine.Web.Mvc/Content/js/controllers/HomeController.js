var HomeController;

HomeController = [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = '首页';
    return $scope.$parent.showBanner = true;
  }
];
