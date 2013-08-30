var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "progressbar", "Article", "Comment", function($scope, $routeParams, progressbar, Article, Comment) {
    $scope.$parent.showBanner = false;
    $scope.loading = true;
    $scope.url = $routeParams.url;
    Article.get({
      $filter: "Url eq '" + $scope.url + "'"
    }, function(data) {
      var i, item, relatedFilter, tag, _i, _j, _len, _len1, _ref, _ref1;
      $scope.loading = false;
      $scope.item = data.value[0];
      if (!$scope.item) {
        $scope.$parent.title = '404';
        return;
      }
      $scope.$parent.title = $scope.item.Title;
      codeformat();
      $scope.entity.PostId = $scope.item.PostId;
      $scope.prevPost = Article.nav({
        $filter: "IsDeleted eq false and CreateDate lt datetime'" + $scope.item.CreateDate + "' and Group/Url eq '" + $scope.item.Group.Url + "'",
        $orderby: 'CreateDate desc'
      });
      $scope.nextPost = Article.nav({
        $filter: "IsDeleted eq false and CreateDate gt datetime'" + $scope.item.CreateDate + "' and Group/Url eq '" + $scope.item.Group.Url + "'",
        $orderby: 'CreateDate'
      });
      if ($scope.item.Tags.length) {
        relatedFilter = '';
        _ref = $scope.item.Tags;
        for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
          tag = _ref[i];
          relatedFilter += " or Tags/any(tag" + i + ":tag" + i + "/Name eq '" + tag.Name + "')";
        }
        relatedFilter = relatedFilter.substring(4);
        relatedFilter = "IsDeleted eq false and PostId ne (guid'" + $scope.item.PostId + "') and (" + relatedFilter + ")";
        $scope.relatedPost = Article.related({
          $filter: relatedFilter,
          $orderby: 'CreateDate desc'
        });
      }
      _ref1 = $scope.item.Comments;
      for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
        item = _ref1[_j];
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
    $scope.$watch('User', function() {
      if ($scope.User) {
        $scope.entity.Author = $scope.User.UserName;
        $scope.entity.Email = $scope.User.Email;
        $scope.entity.Url = $scope.User.Url;
        $scope.AuthorForDisplay = $scope.User.UserName;
        return $scope.editmode = $scope.User.UserName === '' || !($scope.User.UserName != null);
      }
    });
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
    $scope.save = function() {
      progressbar.start();
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
        Article.commented({
          id: "(guid'" + $scope.item.PostId + "')"
        });
        return progressbar.complete();
      }, function(error) {
        var _ref;
        return message.error((_ref = error.data.ExceptionMessage) != null ? _ref : error.status);
      });
    };
    return $scope.remove = function(item) {
      return Comment.remove({
        id: "(guid'" + item.CommentId + "')"
      }, function() {
        item.IsDeleted = true;
        return message.success("Comment has been removed.");
      });
    };
  }
];
