var ArticleListController;

ArticleListController = [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    var _ref, _ref1, _ref2, _ref3;
    $scope.$parent.showBanner = false;
    $scope.currentPage = (_ref = $routeParams.p) != null ? _ref : 1;
    $scope.category = $routeParams.type === 'category' ? $routeParams.query : '';
    $scope.tag = $routeParams.type === 'tag' ? $routeParams.query : '';
    $scope.date = $routeParams.type === 'date' ? $routeParams.query : '';
    $scope.key = (_ref1 = $routeParams.key) != null ? _ref1 : '';
    $scope.$parent.title = (_ref2 = (_ref3 = $routeParams.group) != null ? _ref3 : $routeParams.channel) != null ? _ref2 : "Search Result '" + $scope.key + "'";
    $scope.setPage = function(pageNo) {
      return $location.search({
        p: pageNo
      });
    };
    $scope.load = function() {
      var filter;
      $scope.loading = true;
      filter = '1 eq 1';
      if ($routeParams.channel) {
        filter = "Group/Channel/Url eq '" + $routeParams.channel + "'";
      }
      if ($routeParams.group) filter = "Group/Url eq '" + $routeParams.group + "'";
      if ($routeParams.key) {
        filter = "indexof(Title, '" + $routeParams.key + "') gt 0";
      }
      return Article.query({
        $filter: filter,
        $skip: ($scope.currentPage - 1) * 10,
        category: $scope.category,
        tag: $scope.tag,
        date: $scope.date,
        search: $scope.key
      }, function(data) {
        scroll(0, 0);
        $scope.list = data;
        return $scope.loading = false;
      });
    };
    return $scope.load($scope.currentPage);
  }
];
