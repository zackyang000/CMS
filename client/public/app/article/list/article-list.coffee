angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      title: 'Article list'
      resolve:
        articles: ['$rootScope', '$route', '$q', 'Articles', 'Categories', ($rootScope,$route,$q,Articles,Categories)->
          deferred = $q.defer()
          Categories.main (category) ->
            Articles.query
              $filter: "category eq '#{category.value[0].name}'"
              $skip: ($route.current.params.p || 1) * 10 - 10
              $count: true
              $select: 'title,url,meta,description,date,category,tag'
            ,(data)->
              deferred.resolve data
          deferred.promise
        ]
    .when "/list/:category/tag/:tag",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      title: 'Article list'
      resolve:
        articles: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.query
            $filter: "category eq '#{$route.current.params.category}'" #todo 不支持tag查询
            $skip: ($route.current.params.p || 1) * 10 - 10
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          ,(data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/list/:category",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      title: 'Article list'
      resolve:
        articles: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.query
            $filter: "category eq '#{$route.current.params.category}'"
            $skip: ($route.current.params.p || 1) * 10 - 10
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          ,(data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/search/:key",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      title: 'Article list'
      resolve:
        articles: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Articles.query
            $filter:"indexof(title,'#{$route.current.params.key}') gt -1"
            $skip: ($route.current.params.p || 1) * 10 - 10
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          , (data)->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('ArticleListCtrl',
["$scope","$rootScope","$window","$routeParams","$location","articles", "context"
($scope,$rootScope,$window,$routeParams,$location,articles,context) ->
  $rootScope.$broadcast("categoryChange", $routeParams.category)

  $scope.params = $routeParams

  $window.scroll(0,0)

  $scope.auth = context.auth

  $scope.list = articles
  $scope.currentPage = +$routeParams.p ? 1

  $scope.category = $routeParams.category || articles.value[0].category

  #Turn page
  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.edit = (item) ->
    $window.location.href="/admin/article/#{item._id}"
])
