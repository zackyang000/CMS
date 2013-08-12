var LoginController;

LoginController = [
  "$scope", "User", "$window", function($scope, User, $window) {
    return $scope.login = function() {
      $scope.submitting = true;
      return User.login({
        id: '(1)'
      }, $scope.user, function(data) {
        $scope.submitting = false;
        return $window.location.href = '/admin';
      }, function(error) {
        message.error(error.data['odata.error'].innererror.message);
        return $scope.submitting = false;
      });
    };
  }
];
