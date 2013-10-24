
angular.module('index', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/", {
      templateUrl: "/Content/app/index.tpl.html",
      controller: 'IndexCtrl'
    });
  }
]).controller('IndexCtrl', [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = 'Home';
    $scope.$parent.showBanner = true;
    $scope.loading = "Loading";
    return $http.get("/Content/data/words.js").success(function(data) {
      $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1) - 1];
      return $scope.loading = "";
    });
  }
]);
