
angular.module('index', []).controller('IndexCtrl', [
  "$scope", "$http", function($scope, $http) {
    return $http.get("/Content/data/words.js").success(function(data) {
      return $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1) - 1];
    });
  }
]);
