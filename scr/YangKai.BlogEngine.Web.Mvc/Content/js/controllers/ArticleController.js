var ArticleController;

ArticleController = [
  "$scope", "$http", "$routeParams", function($scope, $http, $routeParams) {
    $scope.$parent.showBanner = false;
    $scope.$parent.loading = true;
    $scope.channel = $routeParams.channel;
    $scope.group = $routeParams.group;
    debugger;
  }
];
