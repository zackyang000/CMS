var LoginController;

LoginController = [
  "$scope", "$window", "User", function($scope, $window, User) {
    $scope.open = function() {
      return $window.location.href = '/admin/';
    };
    $scope.signin = function() {
      $scope.submitting = true;
      return User.signin({
        id: '(1)'
      }, $scope.user, function(data) {
        $scope.submitting = false;
        return $window.location.href = '/admin/';
      }, function(error) {
        $scope.error = error.data['odata.error'].innererror.message;
        $scope.user.Password = '';
        return $scope.submitting = false;
      });
    };
    $scope.signout = function() {
      $scope.submitting = true;
      return User.signout({
        id: '(1)'
      }, function(data) {
        $scope.submitting = false;
        return $window.location.href = '/';
      });
    };
    $scope.manage = function() {
      return $window.location.href = '/admin/';
    };
    return $scope.view = function() {
      return $window.location.href = '/';
    };
  }
];
