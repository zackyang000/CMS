var LoginController, LoginDialogController;

LoginController = [
  "$scope", "$window", "$dialog", "User", function($scope, $window, $dialog, User) {
    $scope.opts = {
      dialogFade: true,
      backdropFade: true,
      templateUrl: '/partials/admin/login-dialog.html',
      controller: 'LoginDialogController'
    };
    $scope.open = function() {
      return $dialog.dialog($scope.opts).open();
    };
    $scope.close = function() {
      return $scope.sgindialog = false;
    };
    $scope.signin = function() {
      $scope.submitting = true;
      return User.signin({
        id: '(1)'
      }, $scope.user, function(data) {
        $scope.submitting = false;
        return $window.location.href = '/admin';
      }, function(error) {
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

LoginDialogController = [
  "$scope", "$window", "dialog", "User", function($scope, $window, dialog, User) {
    $scope.close = function(result) {
      return dialog.close(result);
    };
    return $scope.signin = function() {
      $scope.submitting = true;
      return User.signin({
        id: '(1)'
      }, $scope.user, function(data) {
        $scope.submitting = false;
        return $window.location.href = '/';
      }, function(error) {
        $scope.user.Password = '';
        return $scope.submitting = false;
      });
    };
  }
];
