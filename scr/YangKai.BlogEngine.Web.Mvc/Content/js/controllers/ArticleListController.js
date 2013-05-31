var ArticleListController;

ArticleListController = [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    var _ref, _ref1, _ref2;
    $scope.$parent.showBanner = false;
    $scope.page = (_ref = $routeParams.page) != null ? _ref : 1;
    $scope.channel = (_ref1 = $routeParams.channel) != null ? _ref1 : '';
    $scope.group = (_ref2 = $routeParams.group) != null ? _ref2 : '';
    $scope.category = $routeParams.type === 'category' ? $routeParams.query : '';
    $scope.tag = $routeParams.type === 'tag' ? $routeParams.query : '';
    $scope.date = $routeParams.type === 'date' ? $routeParams.query : '';
    $scope.search = $routeParams.type === 'search' ? $routeParams.query : '';
    $scope.expand = function(item) {
      return item.isShowDetail = !item.isShowDetail;
    };
    $scope.turnpages = function(page) {
      $scope.$parent.loading = true;
      $scope.page = page;
      return $scope.list = Article.querybypaged({
        page: $scope.page,
        channel: $scope.channel,
        group: $scope.group,
        category: $scope.category,
        tag: $scope.tag,
        date: $scope.date,
        search: $scope.search
      }, function() {
        return $scope.$parent.loading = false;
      });
    };
    return $scope.turnpages($scope.page);
  }
];
