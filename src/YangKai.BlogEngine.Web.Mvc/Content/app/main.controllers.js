
angular.module('main.controllers', ['resource.channels', 'resource.users']).controller('GlobalController', [
  "$scope", "$http", "$location", '$window', "Channel", "account", "$timeout", function($scope, $http, $location, $window, Channel, account, $timeout) {
    account.get().then(function(data) {
      return $scope.User = data;
    });
    Channel.query({
      $orderby: 'OrderId',
      $filter: 'IsDeleted eq false',
      $expand: 'Groups',
      $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    }, function(data) {
      $scope.Channels = data.value;
      return $timeout((function() {
        return $('[data-hover="dropdown"]').dropdownHover();
      }), 100);
    });
    $scope.search = function() {
      return $location.path("/search/" + $scope.key);
    };
    return $scope.login = function() {
      return $window.location.href = '/admin/';
    };
  }
]);
