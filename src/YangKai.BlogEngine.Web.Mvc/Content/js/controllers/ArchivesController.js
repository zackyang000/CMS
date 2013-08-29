var ArchivesController;

ArchivesController = [
  "$scope", "Channel", function($scope, Channel) {
    $scope.$parent.title = 'Archives';
    $scope.$parent.showBanner = false;
    $scope.load = function() {
      $scope.loading = true;
      return Channel.archives(function(data) {
        $scope.list = data;
        return $scope.loading = false;
      });
    };
    return $scope.load();
  }
];
