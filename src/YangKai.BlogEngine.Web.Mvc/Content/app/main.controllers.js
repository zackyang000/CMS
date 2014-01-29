
angular.module('main.controllers', ['resource.channels', 'resource.users', "ChannelServices"]).controller('GlobalController', [
  "$scope", "$http", "$location", '$window', "Channel", "account", "$timeout", "channel", function($scope, $http, $location, $window, Channel, account, $timeout, channel) {
    account.get().then(function(data) {
      return $scope.User = data;
    });
    channel.get().then(function(data) {
      return $scope.Channels = data;
    });
    $scope.search = function() {
      return $location.path("/search/" + $scope.key);
    };
    return $scope.login = function() {
      return $window.location.href = '/admin/';
    };
  }
]);
