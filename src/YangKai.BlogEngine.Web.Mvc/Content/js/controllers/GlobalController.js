var GlobalController;

GlobalController = [
  "$scope", "$http", "$location", '$window', function($scope, $http, $location, $window) {
    $http.get("/admin/getuser").success(function(data) {
      debugger;      if (data.Email) {
        data.Gravatar = 'http://www.gravatar.com/avatar/' + md5(data.Email);
      } else {
        data.Gravatar = '/Content/img/avatar.png';
      }
      return $scope.User = data;
    });
    return $scope.search = function() {
      return $window.location.href = "/#!/search/" + $scope.key;
    };
  }
];
