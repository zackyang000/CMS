var AdminGroupController;

AdminGroupController = [
  "$scope", "$dialog", "$routeParams", "Group", "Channel", function($scope, $dialog, $routeParams, Group, Channel) {
    var load;
    Channel.query({
      $filter: "Url eq '" + $routeParams.channel + "'"
    }, function(data) {
      return $scope.channel = data.value[0];
    });
    $scope.entity = {};
    load = function() {
      return Group.query({
        $filter: "IsDeleted eq false and Channel/Url eq '" + $routeParams.channel + "'"
      }, function(data) {
        return $scope.list = data;
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
      $scope.entity.Channel = $scope.channel;
      if ($scope.entity.GroupId) {
        return Group.edit({
          id: "(guid'" + $scope.entity.GroupId + "')"
        }, $scope.entity, function(data) {
          message.success("Edit group successfully.");
          $scope.close();
          return load();
        });
      } else {
        debugger;
        $scope.entity.GroupId = UUID.generate();
        return Group.save($scope.entity, function(data) {
          message.success("Add group successfully.");
          $scope.close();
          return load();
        });
      }
    };
    $scope.remove = function(item) {
      return message.confirm(function() {
        item.IsDeleted = true;
        return Group.edit({
          id: "(guid'" + item.GroupId + "')"
        }, item, function(data) {
          message.success("Delete group successfully.");
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
