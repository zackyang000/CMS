

angular.module('admin.main.controllers', ['resource.users']).controller('GlobalController', [
  "$scope", "$location", "account", "version", "$localStorage", function($scope, $location, account, version, $localStorage) {
    account.get().then(function(data) {
      return $scope.User = data;
    });
    version.get().then(function(data) {
      if (!data.length) {
        return;
      }
      $scope.newVersion = data[0];
      if ($scope.newVersion.ver !== $localStorage.ver) {
        return $scope.newVersion.showDialog = true;
      }
    });
    return $scope.versionClick = function() {
      $localStorage.ver = $scope.newVersion.ver;
      return $scope.newVersion.showDialog = false;
    };
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
