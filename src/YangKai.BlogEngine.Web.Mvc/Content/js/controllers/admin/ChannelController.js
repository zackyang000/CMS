var ChannelController;

ChannelController = [
  "$scope", "$dialog", "Channel", function($scope, $dialog, Channel) {
    var load;
    $scope.loading = true;
    $scope.entity = {};
    load = function() {
      return Channel.query(function(data) {
        $scope.list = data;
        return $scope.loading = false;
      });
    };
    $scope.add = function() {
      $scope.entity = {};
      return $scope.editDialog = true;
    };
    $scope.edit = function(item) {
      $scope.entity = angular.copy(item);
      return $scope.editDialog = true;
    };
    $scope.save = function() {
      if ($scope.entity.ChannelId) {
        return Channel.edit({
          id: "(guid'" + $scope.entity.ChannelId + "')"
        }, $scope.entity, function(data) {
          message.success("Edit channel successfully.");
          $scope.close();
          return load();
        });
      } else {
        $scope.entity.ChannelId = UUID.generate();
        return Channel.save($scope.entity, function(data) {
          message.success("Add channel successfully.");
          $scope.close();
          return load();
        });
      }
    };
    $scope.remove = function(item) {
      return message.confirm(function() {
        item.IsDeleted = true;
        return Channel.edit({
          id: "(guid'" + item.ChannelId + "')"
        }, item, function(data) {
          message.success("Delete channel successfully.");
          return load();
        });
      });
    };
    $scope.close = function() {
      return $scope.editDialog = false;
    };
    return load();
  }
];
