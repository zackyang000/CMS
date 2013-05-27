var BoardController;

BoardController = [
  "$scope", "$http", function($scope, $http) {
    $scope.loading = true;
    $http.get("/board/list").success(function(data) {
      $scope.list = data;
      return $scope.loading = false;
    });
    $scope.del = function(entity) {
      return $http.post("/board/delete", "{id:'" + entity.BoardId + "'}").success(function(data) {
        if (data.result) {
          message.success("“#" + entity.Content + "”  be moved to trash.");
          return entity.IsDeleted = true;
        } else {
          return message.error(data.reason);
        }
      });
    };
    return $scope.renew = function(entity) {
      return $http.post("/board/renew", "{id:'" + entity.BoardId + "'}").success(function(data) {
        if (data.result) {
          message.success("“#" + entity.Content + "”  be moved to trash.");
          return entity.IsDeleted = false;
        } else {
          return message.error(data.reason);
        }
      });
    };
  }
];
