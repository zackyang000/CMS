
angular.module('admin.main.controllers', ['resource.users']).controller('GlobalController', [
  "$scope", "$location", "account", "$localStorage", function($scope, $location, account, $localStorage) {
    return account.get().then(function(data) {
      return $scope.User = data;
    });
  }
]).controller('LoginController', [
  "$scope", "$window", "User", function($scope, $window, User) {
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
    return $scope.backHome = function() {
      return $window.location.href = '/';
    };
  }
]);
