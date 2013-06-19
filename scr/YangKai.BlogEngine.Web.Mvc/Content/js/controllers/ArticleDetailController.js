var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", "Comment", function($scope, $routeParams, Article, Comment) {
    $scope.$parent.showBanner = false;
    $scope.loading = true;
    $scope.url = $routeParams.url;
    $scope.item = Article.get({
      id: $scope.url
    }, function() {
      $scope.$parent.title = $scope.item.Title;
      $scope.loading = false;
      codeformat();
      $scope.entity.PostId = $scope.item.PostId;
      $scope.nav = Article.nav({
        id: $scope.item.PostId
      });
      $scope.related = Article.related({
        id: $scope.item.PostId
      });
      return $scope.list = Comment.query({
        PostId: $scope.item.PostId
      });
    });
    $scope.entity = {};
    $scope.entity.Author = $scope.Name;
    $scope.entity.Email = $scope.Email;
    $scope.entity.Url = $scope.Url;
    $scope.AuthorForDisplay = $scope.Name;
    $scope.editmode = $scope.Name === '' || !($scope.Name != null);
    $scope.del = function(item) {
      return Comment.del({
        id: item.CommentId
      }, function(data) {
        message.success("“#" + item.Content + "”  be moved to trash.");
        return item.IsDeleted = true;
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
    $scope.renew = function(item) {
      return Comment.renew({
        id: item.CommentId
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
      return Comment.add($scope.entity, function(data) {
        message.success("Message has been submitted.");
        $scope.list.push(data);
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
