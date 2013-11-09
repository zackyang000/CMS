
angular.module('about', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/about", {
      templateUrl: "/Content/app/about/about.tpl.html",
      controller: 'AboutCtrl'
    });
  }
]).controller('AboutCtrl', [
  "$scope", "$translate", "$http", function($scope, $translate, $http) {
    $scope.$parent.title = 'About';
    return $scope.$parent.showBanner = false;
  }
]);
