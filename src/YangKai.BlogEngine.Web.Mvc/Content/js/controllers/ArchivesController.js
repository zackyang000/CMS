var ArchivesController;

ArchivesController = [
  "$scope", "Channel", function($scope, Channel) {
    $scope.$parent.title = 'Archives';
    $scope.$parent.showBanner = false;
    $scope.load = function() {
      return Channel.archives(function(data) {
        return $scope.list = data;
      });
    };
    return $scope.load();
  }
];
