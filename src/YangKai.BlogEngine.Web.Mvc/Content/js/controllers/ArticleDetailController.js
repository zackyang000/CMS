var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", "Comment", function($scope, $routeParams, Article, Comment) {
    $scope.$parent.showBanner = false;
    $scope.loading = true;
    $scope.url = $routeParams.url;
    Article.get({
      $filter: "Url eq '" + $scope.url + "'"
    }, function(data) {
      var item;
      $scope.item = data.value[0];
      $scope.$parent.title = $scope.item.Title;
      codeformat();
      $scope.loading = false;
      $scope.entity.PostId = $scope.item.PostId;
      $scope.nav = Article.nav({
        id: $scope.item.PostId
      });
      return $scope.related = Article.related({
        id: $scope.item.PostId
      }, (function() {
        var _i, _len, _ref, _results;
        _ref = $scope.item.Comments;
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          if (item.Email) {
            _results.push(item.Gravatar = 'http://www.gravatar.com/avatar/' + md5(item.Email));
          } else {
            _results.push(item.Gravatar = '/Content/img/avatar.png');
          }
        }
        return _results;
      })());
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
      return Comment.save($scope.entity, function(data) {
        message.success("Comment has been submitted.");
        $scope.item.Comments.push(data);
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
