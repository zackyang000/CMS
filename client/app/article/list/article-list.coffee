angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$rootScope', '$route', '$q', 'Articles', 'Categories', ($rootScope,$route,$q,Articles,Categories)->
          deferred = $q.defer()
          Categories.main (category) ->
            Articles.query
              category: category.name
            ,(data)->
              deferred.resolve data
          deferred.promise
        ]
    .when "/list/:category/tag/:tag",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.query
            category: $route.current.params.category
            tag: $route.current.params.tag
          ,(data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/list/:category",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.query
            category: $route.current.params.category
          ,(data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/search/:key",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Article.queryOnce
            $filter:"""
            IsDeleted eq false
            and indexof(Title, '#{route.current.params.key}') gt -1
            """
            $skip:($route.current.params.p ? 1)*10 - 10
          , (data)->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('ArticleListCtrl',
["$scope","$rootScope","$window","$routeParams","$location","articles", "context"
($scope,$rootScope,$window,$routeParams,$location,articles,context) ->
  $window.scroll(0,0)
  $scope.isAdmin = context.auth.admin

  $scope.list = articles
  $scope.currentPage =$routeParams.p ? 1

  $scope.params=$routeParams

  #Turn page
  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.edit = (item) ->
    $window.location.href="/admin/article/#{item._id}"
])
