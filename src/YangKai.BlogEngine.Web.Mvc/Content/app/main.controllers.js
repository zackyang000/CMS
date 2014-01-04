
angular.module('main.controllers', ['resource.channels', 'resource.users']).controller('GlobalController', [
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
    return $scope.login = function() {
      return $window.location.href = '/admin/';
    };
  }
]);
