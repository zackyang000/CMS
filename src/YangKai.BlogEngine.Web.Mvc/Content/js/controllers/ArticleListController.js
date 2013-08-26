var ArticleListController;

ArticleListController = [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    var _ref, _ref1, _ref2;
    $scope.$parent.showBanner = false;
    $scope.$parent.title = (_ref = (_ref1 = $routeParams.group) != null ? _ref1 : $routeParams.channel) != null ? _ref : "Search Result '" + $scope.key + "'";
    debugger;
    $scope.currentPage = (_ref2 = $routeParams.p) != null ? _ref2 : 1;
    $scope.category = $routeParams.type === 'category' ? $routeParams.query : '';
    $scope.tag = $routeParams.type === 'tag' ? $routeParams.query : '';
    $scope.channel = $routeParams.channel;
    $scope.group = $routeParams.group;
    $scope.keyword = $routeParams.key;
    $scope.setPage = function(pageNo) {
      return $location.search({
        p: pageNo
      });
    };
    $scope.load = function() {
      var filter;
      $scope.loading = true;
      filter = 'IsDeleted eq false';
      if ($routeParams.channel) {
        filter += " and Group/Channel/Url eq '" + $routeParams.channel + "'";
      }
      if ($routeParams.group) {
        filter += " and Group/Url eq '" + $routeParams.group + "'";
      }
      if ($routeParams.key) {
        filter += " and indexof(Title, '" + $routeParams.key + "') gt 0";
      }
      if ($scope.category) {
        filter += " and Categorys/any(category:category/Url eq '" + $scope.category + "')";
      }
      if ($scope.tag) {
        filter += " and Tags/any(tag:tag/Name eq '" + $scope.tag + "')";
      }
      return Article.query({
        $filter: filter,
        $skip: ($scope.currentPage - 1) * 10
      }, function(data) {
        scroll(0, 0);
        $scope.list = data;
        return $scope.loading = false;
      });
    };
    return $scope.load($scope.currentPage);
  }
];
