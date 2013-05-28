var BoardController;

BoardController = [
  "$scope", "$http", "Message", function($scope, $http, Message) {
    $scope.entity = {};
    $scope.isAdmin = false;
    $scope.loading = true;
    $http.get("/comment/UserInfo").success(function(data) {
      $scope.entity.Author = data.Author;
      $scope.entity.Email = data.Email;
      $scope.entity.Url = data.Url;
      $scope.AuthorForDisplay = data.Author;
      return $scope.editmode = data.Author === '';
    });
    $scope.list = Message.query(function() {
      return $scope.loading = false;
    });
    $scope.del = function(item) {
      return $http.post("/board/delete", "{id:'" + item.BoardId + "'}").success(function(data) {
        if (data.result) {
          message.success("“#" + item.Content + "”  be moved to trash.");
          return item.IsDeleted = true;
        } else {
          return message.error(data.reason);
        }
      });
    };
    $scope.renew = function(item) {
      return $http.post("/board/renew", "{id:'" + item.BoardId + "'}").success(function(data) {
        if (data.result) {
          message.success("“#" + item.Content + "”  be moved to trash.");
          return item.IsDeleted = false;
        } else {
          return message.error(data.reason);
        }
      });
    };
    return $scope.save = function() {
      $scope.submitting = true;
      return $http.post("/board/add", $scope.entity).success(function(data) {
        if (data.result) {
          message.success("Message has been submitted.");
          $scope.list.unshift(data.model);
          $scope.entity.Content = "";
          $scope.AuthorForDisplay = data.model.Author;
          $scope.editmode = false;
          angular.resetForm($scope, 'form');
        } else {
          message.error(data.reason);
        }
        return $scope.submitting = false;
      });
    };
  }
];
