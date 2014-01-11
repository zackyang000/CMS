
angular.module('article-detail', ['resource.articles', 'resource.comments']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/post/:url", {
      templateUrl: "/Content/app/article/detail/article-detail.tpl.html",
      controller: 'ArticleDetailCtrl',
      resolve: {
        article: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred;
            deferred = $q.defer();
            Article.getOnce({
              $filter: "Url eq '" + $route.current.params.url + "' and IsDeleted eq false",
              $expand: 'Tags,Group/Channel,Comments'
            }, function(data) {
              return deferred.resolve(data.value[0]);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('ArticleDetailCtrl', [
  "$scope", "$window", "$translate", "$routeParams", "progressbar", "Article", "Comment", "article", "account", function($scope, $window, $translate, $routeParams, progressbar, Article, Comment, article, account) {
    var i, item, relatedFilter, tag, _i, _j, _len, _len1, _ref, _ref1;
    $scope.$parent.showBanner = false;
    $scope.item = article;
    if (!$scope.item) {
      $scope.$parent.title = '404';
      return;
    }
    $scope.$parent.title = $scope.item.Title;
    codeformat();
    $scope.prevPost = Article.getOnce({
      $select: 'Url,Title',
      $filter: "IsDeleted eq false and CreateDate lt datetime'" + $scope.item.CreateDate + "' and Group/Url eq '" + $scope.item.Group.Url + "'",
      $orderby: 'CreateDate desc'
    });
    $scope.nextPost = Article.getOnce({
      $select: 'Url,Title',
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
      $scope.relatedPost = Article.getOnce({
        $top: 8,
        $select: 'Url,Title,PubDate',
        $filter: relatedFilter,
        $orderby: 'CreateDate desc'
      });
    }
    _ref1 = $scope.item.Comments;
    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
      item = _ref1[_j];
      if (!item.Avatar) {
        if (item.Email) {
          item.Avatar = 'http://www.gravatar.com/avatar/' + md5(item.Email);
        } else {
          item.Avatar = '/Content/img/avatar.png';
        }
      }
    }
    Article.browsed({
      id: "(guid'" + $scope.item.PostId + "')"
    });
    account.get().then(function(data) {
      $scope.entity = {
        Author: data.UserName,
        Email: data.Email,
        Url: data.Url
      };
      return $scope.editmode = !data.UserName;
    });
    $scope.del = function(item) {
      return message.confirm(function() {
        return Comment.del({
          id: item.CommentId
        }, function(data) {
          message.success("“#" + item.Content + "”  be moved to trash.");
          return item.IsDeleted = true;
        }, function(error) {
          var _ref2;
          return message.error((_ref2 = error.data.ExceptionMessage) != null ? _ref2 : error.status);
        });
      });
    };
    $scope.save = function() {
      $scope.submitted = true;
      if ($scope.form.$invalid) {
        return;
      }
      progressbar.start();
      $scope.loading = $translate("global.post");
      $scope.entity.CommentId = UUID.generate();
      $scope.entity.Post = {
        PostId: $scope.item.PostId
      };
      return Comment.save($scope.entity, function(data) {
        message.success($translate("article.comment.complete"));
        $scope.item.Comments.push(data);
        $scope.entity.Content = "";
        Article.commented({
          id: "(guid'" + $scope.item.PostId + "')"
        });
        progressbar.complete();
        $scope.submitted = false;
        return $scope.loading = "";
      }, function(error) {
        $scope.submitted = false;
        return $scope.loading = "";
      });
    };
    $scope.remove = function(item) {
      return message.confirm(function() {
        return Comment.remove({
          id: "(guid'" + item.CommentId + "')"
        }, function() {
          item.IsDeleted = true;
          return message.success("Comment has been removed.");
        });
      });
    };
    return $scope.edit = function(item) {
      return $window.location.href = "/admin/article('" + item.PostId + "')";
    };
  }
]);
