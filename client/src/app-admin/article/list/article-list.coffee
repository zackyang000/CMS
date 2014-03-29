angular.module('admin-article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/article",
      templateUrl: "/app-admin/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
])

.controller('ArticleListCtrl',
["$scope","$routeParams","$location","Article", 
($scope,$routeParams,$location,Article) ->
  $scope.setPage = (pageNo) ->
    $scope.loading="Loading"
    Article.query
      $filter:'IsDeleted eq false and Group/IsDeleted eq false and Group/Channel/IsDeleted eq false'
      $skip:(pageNo-1)*10
    , (data)->
      $scope.list = data
      $scope.loading=""

  $scope.setPage 1
])