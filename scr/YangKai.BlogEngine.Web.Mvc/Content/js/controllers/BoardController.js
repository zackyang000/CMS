var BoardController;

BoardController = [
  "$scope", "Message", "$http", function($scope, Message, $http) {
    $scope.$parent.showBanner = false;
    $scope.$parent.loading = true;
    $scope.entity = {};
    $scope.entity.Author = $scope.Name;
    $scope.entity.Email = $scope.Email;
    $scope.entity.Url = $scope.Url;
    $scope.AuthorForDisplay = $scope.Name;
    $scope.editmode = $scope.Name === '' || !($scope.Name != null);
    $scope.list = Message.query(function() {
      return $scope.$parent.loading = false;
    });
    $scope.del = function(item) {
      return Message.del({
        id: item.BoardId
      }, function(data) {
        message.success("“#" + item.Content + "”  be moved to trash.");
        return item.IsDeleted = true;
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
    $scope.renew = function(item) {
      return Message.renew({
        id: item.BoardId
      }, function(data) {
        message.success("“#" + item.Content + "”  be renew.");
        return item.IsDeleted = false;
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
    return $scope.save = function() {
      $scope.submitting = true;
      return Message.add($scope.entity, function(data) {
        message.success("Message has been submitted.");
        $scope.list.unshift(data);
        $scope.entity.Content = "";
        $scope.AuthorForDisplay = data.Author;
        $scope.editmode = false;
        angular.resetForm($scope, 'form');
        return $scope.submitting = false;
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
  }
];
