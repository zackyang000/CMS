
angular.module('index', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/", {
      templateUrl: "/Content/app/index.tpl.html",
      controller: 'IndexCtrl',
      title: 'Home',
      showBanner: true
    });
  }
]).controller('IndexCtrl', [
  "$scope", "$http", function($scope, $http) {
    return $http.get("/Content/data/words.js").success(function(data) {
      return $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1) - 1];
    });
  }
]);
