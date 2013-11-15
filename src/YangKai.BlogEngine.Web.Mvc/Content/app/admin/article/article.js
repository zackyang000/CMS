
angular.module('admin-article', ['admin-article-edit']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/admin/article", {
      templateUrl: "/Content/app/admin/article/article.tpl.html",
      controller: 'ArticleListCtrl'
    });
  }
]).controller('ArticleListCtrl', [
  "$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {
    $scope.setPage = function(pageNo) {
      $scope.loading = "Loading";
      return Article.query({
        $filter: 'IsDeleted eq false and Group/IsDeleted eq false and Group/Channel/IsDeleted eq false',
        $skip: (pageNo - 1) * 10
      }, function(data) {
        $scope.list = data;
        return $scope.loading = "";
      });
    };
    return $scope.setPage(1);
  }
]);
