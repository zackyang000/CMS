var ChannelController;

ChannelController = [
  "$scope", "$dialog", "Channel", function($scope, $dialog, Channel) {
    $scope.loading = true;
    Channel.query(function(data) {
      return $scope.list = data;
    });
    $scope.opts = {
      backdrop: true,
      keyboard: true,
      backdropClick: true,
      dialogFade: true,
      backdropFade: true
    };
    $scope.open = function() {
      return $scope.shouldBeOpen = true;
    };
    return $scope.close = function() {
      $scope.closeMsg = 'I was closed at: ' + new Date();
      return $scope.shouldBeOpen = false;
    };
  }
];
