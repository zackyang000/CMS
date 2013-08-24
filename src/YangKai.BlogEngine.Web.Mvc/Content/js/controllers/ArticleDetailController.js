var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", "Comment", function($scope, $routeParams, Article, Comment) {
    $scope.$parent.showBanner = false;
    $scope.loading = true;
    $scope.url = $routeParams.url;
    Article.get({
      $filter: "Url eq '" + $scope.url + "'"
    }, function(data) {
      var item, _i, _len, _ref;
      $scope.item = data.value[0];
      $scope.$parent.title = $scope.item.Title;
      codeformat();
      $scope.loading = false;
      $scope.entity.PostId = $scope.item.PostId;
      $scope.prevPost = Article.nav({
        $filter: "CreateDate lt datetime'" + $scope.item.CreateDate + "' and Group/Url eq '" + $scope.item.Group.Url + "'",
        $orderby: 'CreateDate desc'
      });
      $scope.nextPost = Article.nav({
        $filter: "CreateDate gt datetime'" + $scope.item.CreateDate + "' and Group/Url eq '" + $scope.item.Group.Url + "'",
        $orderby: 'CreateDate'
      });
      _ref = $scope.item.Comments;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.Email) {
          item.Gravatar = 'http://www.gravatar.com/avatar/' + md5(item.Email);
        } else {
          item.Gravatar = '/Content/img/avatar.png';
        }
      }
      return Article.browsed({
        id: "(guid'" + $scope.item.PostId + "')"
      });
    });
    $scope.entity = {};
    $scope.entity.Author = $scope.User.UserName;
    $scope.entity.Email = $scope.User.Email;
    $scope.entity.Url = $scope.User.Url;
    $scope.AuthorForDisplay = $scope.User.UserName;
    $scope.editmode = $scope.User.UserName === '' || !($scope.User.UserName != null);
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
      $scope.entity.CommentId = UUID.generate();
      return Comment.save($scope.entity, function(data) {
        message.success("Comment has been submitted.");
        $scope.item.Comments.push(data);
        $scope.entity.Content = "";
        $scope.AuthorForDisplay = data.Author;
        $scope.editmode = false;
        angular.resetForm($scope, 'form');
        $scope.submitting = false;
        return Article.commented({
          id: "(guid'" + $scope.item.PostId + "')"
        });
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
  }
];
