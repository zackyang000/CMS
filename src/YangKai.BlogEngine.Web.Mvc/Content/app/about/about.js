
angular.module('about', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/about", {
      templateUrl: "/Content/app/about/about-newegg.tpl.html",
      controller: 'AboutCtrl'
    });
  }
]).controller('AboutCtrl', [
  "$scope", "$translate", "$http", function($scope, $translate, $http) {
    $scope.$parent.title = 'About';
    $scope.$parent.showBanner = false;
    $scope.loading = $translate("global.loading");
    return $http.get("/Content/data/technology.js").success(function(data) {
      $scope.list = data;
      return $scope.loading = "";
    });
  }
]);
