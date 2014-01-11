
angular.module('article-list', ['resource.articles']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/list/:channel/:group/:type/:query", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    }).when("/list/:channel/:group", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    }).when("/list/:channel", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl',
      resolve: {
        articles: [
          '$route', '$q', 'Article', function($route, $q, Article) {
            var deferred, filter, _ref;
            deferred = $q.defer();
            filter = 'IsDeleted eq false';
            Article.queryOnce({
              $filter: "IsDeleted eq false and Group/Channel/Url eq '" + $route.current.params.channel + "'",
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
      controller: 'ArticleListCtrl'
    });
  }
]).controller('ArticleListCtrl', [
  "$scope", "$window", "$translate", "$routeParams", "$location", "Article", "articles", function($scope, $window, $translate, $routeParams, $location, Article, articles) {
    var _ref, _ref1, _ref2;
    $scope.$parent.showBanner = false;
    $scope.$parent.title = (_ref = (_ref1 = $routeParams.group) != null ? _ref1 : $routeParams.channel) != null ? _ref : "Search Result '" + $scope.key + "'";
    $scope.currentPage = (_ref2 = $routeParams.p) != null ? _ref2 : 1;
    $scope.params = {
      channel: $routeParams.channel,
      group: $routeParams.group,
      key: $routeParams.key,
      tag: $routeParams.type === 'tag' ? $routeParams.query : ''
    };
    $scope.setPage = function(pageNo) {
      return $location.search({
        p: pageNo
      });
    };
    scroll(0, 0);
    $scope.list = articles;
    return $scope.edit = function(item) {
      return $window.location.href = "/admin/article('" + item.PostId + "')";
    };
  }
]);
