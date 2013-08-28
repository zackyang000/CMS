var HomeController;

HomeController = [
  "$scope", "$http", function($scope, $http) {
    $scope.$parent.title = 'Home';
    $scope.$parent.showBanner = true;
    return $http.get("/data/words.js").success(function(data) {
      return $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1) - 1];
    });
  }
];
