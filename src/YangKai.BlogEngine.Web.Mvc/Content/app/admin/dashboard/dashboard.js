
angular.module('admin-dashboard', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/", {
      templateUrl: "/content/app/admin/dashboard/dashboard.tpl.html",
      controller: 'DashboardCtrl'
    });
  }
]).controller('DashboardCtrl', ["$scope", "$http", function($scope, $http) {}]);
