var ChannelController;

ChannelController = [
  "$scope", "Channel", function($scope, Channel) {
    $scope.loading = true;
    return Channel.query(function(data) {
      $scope.list = data;
      return $scope.loading = false;
    });
  }
];
