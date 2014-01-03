
angular.module('admin-system-history', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/admin/system/history", {
      templateUrl: "/content/app/admin/system/history/history.tpl.html",
      controller: 'SystemHistoryCtrl'
    });
  }
]).controller('SystemHistoryCtrl', [
  "$scope", "$http", 'VersionService', function($scope, $http, VersionService) {
    return VersionService.get().then(function(data) {
      return $scope.versions = data;
    });
  }
]);
