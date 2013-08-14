var GlobalController;

GlobalController = [
  "$scope", "$http", "$location", '$window', function($scope, $http, $location, $window) {
    $http.get("/admin/getuser").success(function(data) {
      if (data.Email) {
        data.Gravatar = 'http://www.gravatar.com/avatar/' + md5(data.Email);
      } else {
        data.Gravatar = '/Content/img/avatar.png';
      }
      return $scope.User = data;
    });
    $scope.manage = function() {
      return $window.location.href = '/admin/';
    };
    $scope.signin = function() {};
    $scope.signout = function() {};
    return $scope.search = function() {
      return $window.location.href = "/#!/search/" + $scope.key;
    };
  }
];
