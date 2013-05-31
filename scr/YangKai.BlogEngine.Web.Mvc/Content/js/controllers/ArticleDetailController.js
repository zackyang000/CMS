var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", function($scope, $routeParams, Article) {
    $scope.$parent.showBanner = false;
    $scope.$parent.loading = true;
    $scope.url = $routeParams.url;
    return $scope.item = Article.get({
      id: $scope.url
    }, function() {
      return $scope.$parent.loading = false;
    });
  }
];
