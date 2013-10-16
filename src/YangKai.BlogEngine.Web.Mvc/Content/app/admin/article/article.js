
angular.module('admin-article', ['admin-article-edit']).controller('ArticleListCtrl', [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    $scope.setPage = function(pageNo) {
      return Article.query({
        $filter: 'Group/IsDeleted eq false and Group/Channel/IsDeleted eq false',
        $skip: (pageNo - 1) * 10
      }, function(data) {
        return $scope.list = data;
      });
    };
    return $scope.setPage(1);
  }
]);
