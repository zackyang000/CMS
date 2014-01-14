
angular.module('article-list', ['resource.articles']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/list/:channel/:group/tag/:tag", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl',
      resolve: {
        articles: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred, _ref;
            deferred = $q.defer();
            Article.queryOnce({
              $filter: "IsDeleted eq false \nand Group/Channel/Url eq '" + $route.current.params.channel + "' \nand Tags/any(tag:tag/Name eq '" + $route.current.params.tag + "')",
              $skip: ((_ref = $route.current.params.p) != null ? _ref : 1) * 10 - 10
            }, function(data) {
              return deferred.resolve(data);
            });
            return deferred.promise;
          }
        ]
      }
    }).when("/list/:channel/:group", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl',
      resolve: {
        articles: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred, _ref;
            deferred = $q.defer();
            Article.queryOnce({
              $filter: "IsDeleted eq false \nand Group/Channel/Url eq '" + $route.current.params.channel + "' \nand Group/Url eq '" + $route.current.params.group + "'",
              $skip: ((_ref = $route.current.params.p) != null ? _ref : 1) * 10 - 10
            }, function(data) {
              return deferred.resolve(data);
            });
            return deferred.promise;
          }
        ]
      }
    }).when("/list/:channel", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl',
      resolve: {
        articles: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred, _ref;
            deferred = $q.defer();
            Article.queryOnce({
              $filter: "IsDeleted eq false \nand Group/Channel/Url eq '" + $route.current.params.channel + "'",
              $skip: ((_ref = $route.current.params.p) != null ? _ref : 1) * 10 - 10
            }, function(data) {
              return deferred.resolve(data);
            });
            return deferred.promise;
          }
        ]
      }
    }).when("/search/:key", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl',
      resolve: {
        articles: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred, _ref;
            deferred = $q.defer();
            Article.queryOnce({
              $filter: "IsDeleted eq false \nand indexof(Title, '" + $route.current.params.key + "') gt -1",
              $skip: ((_ref = $route.current.params.p) != null ? _ref : 1) * 10 - 10
            }, function(data) {
              return deferred.resolve(data);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('ArticleListCtrl', [
  "$scope", "$rootScope", "$window", "$routeParams", "$location", "articles", function($scope, $rootScope, $window, $routeParams, $location, articles) {
    var _ref, _ref1, _ref2;
    scroll(0, 0);
    $scope.$parent.showBanner = false;
    $rootScope.title = (_ref = (_ref1 = (_ref2 = $routeParams.tag) != null ? _ref2 : $routeParams.group) != null ? _ref1 : $routeParams.channel) != null ? _ref : "Search Result '" + $scope.key + "'";
    $scope.list = articles;
    $scope.params = $routeParams;
    $scope.setPage = function(pageNo) {
      return $location.search({
        p: pageNo
      });
    };
    return $scope.edit = function(item) {
      return $window.location.href = "/admin/article('" + item.PostId + "')";
    };
  }
]);
