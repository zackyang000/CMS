angular.module('admin-article',['admin-article-edit'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/article",
    templateUrl: "/Content/app/admin/article/article.tpl.html"
    controller: 'ArticleListCtrl')
])

.controller('ArticleListCtrl',
["$scope","$routeParams","$location","Article", 
($scope,$routeParams,$location,Article) ->
  $scope.setPage = (pageNo) ->
    $scope.loading="Loading"
    Article.query
      $filter:'Group/IsDeleted eq false and Group/Channel/IsDeleted eq false'
      $skip:(pageNo-1)*10
    , (data)->
      $scope.list = data
      $scope.loading=""

  $scope.setPage 1
])