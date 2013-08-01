var ArticleListController;

ArticleListController = [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    var _ref, _ref1, _ref2, _ref3, _ref4;
    $scope.$parent.showBanner = false;
    $scope.page = (_ref = $routeParams.page) != null ? _ref : 1;
    $scope.channel = (_ref1 = $routeParams.channel) != null ? _ref1 : '';
    $scope.group = (_ref2 = $routeParams.group) != null ? _ref2 : '';
    $scope.category = $routeParams.type === 'category' ? $routeParams.query : '';
    $scope.tag = $routeParams.type === 'tag' ? $routeParams.query : '';
    $scope.date = $routeParams.type === 'date' ? $routeParams.query : '';
    $scope.key = (_ref3 = $routeParams.key) != null ? _ref3 : '';
    $scope.$parent.title = (_ref4 = $routeParams.group) != null ? _ref4 : $routeParams.channel;
    $scope.expand = function(item) {
      item.isShowDetail = !item.isShowDetail;
      return codeformat();
    };
    $scope.turnpages = function(page) {
      var result;
      $scope.loading = true;
      $scope.page = page;
      return result = Article.query({
        page: $scope.page,
        channel: $scope.channel,
        group: $scope.group,
        category: $scope.category,
        tag: $scope.tag,
        date: $scope.date,
        search: $scope.key
      }, function() {
        if (page === 1) {
          $scope.list = result;
        } else {
          $scope.list = $scope.list.concat(result);
        }
        return $scope.loading = false;
      });
    };
    return $scope.turnpages($scope.page);
  }
];
