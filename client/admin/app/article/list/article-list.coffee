angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/article",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
])

.controller('ArticleListCtrl',
["$scope","$routeParams","$location","Articles",
($scope,$routeParams,$location,Articles) ->
  $scope.setPage = (pageNo) ->
    $scope.loading="Loading"
    Articles.query (data) ->
      $scope.list = data.value
      $scope.loading=""

  $scope.setPage 1
])