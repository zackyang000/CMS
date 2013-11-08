
angular.module('article-list', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/list/:channel/:group/:type/:query", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    }).when("/list/:channel/:group", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    }).when("/list/:channel", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    }).when("/search/:key", {
      templateUrl: "/Content/app/article/list/article-list.tpl.html",
      controller: 'ArticleListCtrl'
    });
  }
]).controller('ArticleListCtrl', [
  "$scope", "$window", "$translate", "$routeParams", "$location", "Article", function($scope, $window, $translate, $routeParams, $location, Article) {
    var _ref, _ref1, _ref2;
    $scope.$parent.showBanner = false;
    $scope.$parent.title = (_ref = (_ref1 = $routeParams.group) != null ? _ref1 : $routeParams.channel) != null ? _ref : "Search Result '" + $scope.key + "'";
    $scope.currentPage = (_ref2 = $routeParams.p) != null ? _ref2 : 1;
    $scope.params = {
      channel: $routeParams.channel,
      group: $routeParams.group,
      key: $routeParams.key,
      category: $routeParams.type === 'category' ? $routeParams.query : '',
      tag: $routeParams.type === 'tag' ? $routeParams.query : ''
    };
    $scope.setPage = function(pageNo) {
      return $location.search({
        p: pageNo
      });
    };
    $scope.load = function() {
      var filter;
      $scope.loading = "Loading";
      filter = 'IsDeleted eq false';
      if ($scope.params.channel) {
        filter += " and Group/Channel/Url eq '" + $scope.params.channel + "'";
      }
      if ($scope.params.group) {
        filter += " and Group/Url eq '" + $scope.params.group + "'";
      }
      if ($scope.params.key) {
        filter += " and indexof(Title, '" + $scope.params.key + "') gt -1";
      }
      if ($scope.params.category) {
        filter += " and Categorys/any(category:category/Url eq '" + $scope.params.category + "')";
      }
      if ($scope.params.tag) {
        filter += " and Tags/any(tag:tag/Name eq '" + $scope.params.tag + "')";
      }
      return Article.query({
        $filter: filter,
        $skip: ($scope.currentPage - 1) * 10
      }, function(data) {
        scroll(0, 0);
        $scope.list = data;
        return $scope.loading = "";
      });
    };
    $scope.edit = function(item) {
      return $window.location.href = "/admin/#!/article('" + item.PostId + "')";
    };
    return $scope.load($scope.currentPage);
  }
]);
