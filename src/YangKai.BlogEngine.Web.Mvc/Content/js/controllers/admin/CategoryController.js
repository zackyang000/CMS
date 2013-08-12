var CategoryController;

CategoryController = [
  "$scope", "$routeParams", "Category", "Group", function($scope, $routeParams, Category, Group) {
    $scope.loading = true;
    Category.query({
      $filter: "IsDeleted eq false and Group/Url eq '" + $routeParams.group + "'"
    }, function(data) {
      $scope.list = data;
      return $scope.loading = false;
    });
    return Group.query({
      $filter: "Url eq '" + $routeParams.group + "'",
      $expand: 'Channel'
    }, function(data) {
      return $scope.group = data.value[0];
    });
  }
];
