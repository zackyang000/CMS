
angular.module('ctrl.main', ['resource.channels', 'resource.users']).controller('GlobalController', [
  "$scope", "$http", "$location", '$window', "Channel", "account", function($scope, $http, $location, $window, Channel, account) {
    account.get().then(function(data) {
      return $scope.User = data;
    });
    Channel.query({
      $orderby: 'OrderId',
      $filter: 'IsDeleted eq false',
      $expand: 'Groups',
      $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    }, function(data) {
      return $scope.Channels = data.value;
    });
    $scope.search = function() {
      return $location.path("/search/" + $scope.key);
    };
    return $scope.GoHome = function() {
      return $window.location.href = '/';
    };
  }
]).controller('LoginController', [
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
]);
