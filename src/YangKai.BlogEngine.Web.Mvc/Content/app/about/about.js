
angular.module('about', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/about", {
      templateUrl: "/Content/app/about/about-newegg.tpl.html",
      controller: 'AboutCtrl'
    });
  }
]).controller('AboutCtrl', [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = 'About';
    $scope.$parent.showBanner = false;
    $scope.loading = "Loading";
    return $http.get("/Content/data/technology.js").success(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
]);
