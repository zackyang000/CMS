var GroupController;

GroupController = [
  "$scope", "$routeParams", "Group", "Channel", function($scope, $routeParams, Group, Channel) {
    $scope.loading = true;
    Group.query({
      $filter: "IsDeleted eq false and Channel/Url eq '" + $routeParams.channel + "'"
    }, function(data) {
      $scope.list = data;
      return $scope.loading = false;
    });
    return Channel.query({
      $filter: "Url eq '" + $routeParams.channel + "'"
    }, function(data) {
      return $scope.channel = data.value[0];
    });
  }
];
