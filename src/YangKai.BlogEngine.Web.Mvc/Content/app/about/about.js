
angular.module('about', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/about", {
      templateUrl: "/Content/app/about/about.tpl.html",
      controller: 'AboutCtrl',
      title: 'About'
    });
  }
]).controller('AboutCtrl', ["$scope", "$translate", "$http", function($scope, $translate, $http) {}]);
