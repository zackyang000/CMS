var GlobalController;

GlobalController = [
  "$scope", "$http", "$location", '$window', "Channel", "account", function($scope, $http, $location, $window, Channel, account) {
    Channel.query({
      $orderby: 'OrderId',
      $filter: 'IsDeleted eq false',
      $expand: 'Groups',
      $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    }, function(data) {
      return $scope.Channels = data.value;
    });
    return $scope.search = function() {
      return $location.path("/search/" + $scope.key);
    };
  }
];
