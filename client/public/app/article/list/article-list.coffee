angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route', '$q', 'Articles', 'Categories', ($route, $q, Articles, Categories) ->
          deferred = $q.defer()
          Categories.main (category) ->
            Articles.query
              $filter: "category eq '#{category.value[0].url}'"
              $select: 'title,url,meta,description,date,category,tag'
              $top: 10
              $skip: ($route.current.params.p || 1) * 10 - 10
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
            $filter: "category eq '#{$route.current.params.category}'" #todo 不支持tag查询
            $top: 10
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
            $top: 10
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
      resolve:
        articles: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Articles.query
            $filter:"indexof(title,'#{$route.current.params.key}') gt -1"
            $top: 10
            $skip: ($route.current.params.p || 1) * 10 - 10
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          , (data)->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('ArticleListCtrl',
["$scope","$rootScope","$window","$routeParams","$location","articles", "context", 'dataCacheCategories'
($scope,$rootScope,$window,$routeParams,$location,articles,context, dataCacheCategories) ->
  $rootScope.$broadcast("categoryChange", $routeParams.category)

  $scope.params = $routeParams

  $window.scroll(0,0)

  $scope.auth = context.auth

  $scope.list = articles

  unless $routeParams.key
    if $routeParams.category
      for category in dataCacheCategories
        if $routeParams.category  == category.url
          $scope.category = category
          break
    else
      for category in dataCacheCategories
        if category.main
          $scope.category = category
          break
    $rootScope.title = $scope.category.name[context.language]

  #Turn page
  $scope.currentPage = +$routeParams.p || 1
  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.edit = (item) ->
    $window.location.href = "#{config.url.admin}/article/#{item._id}"
])
