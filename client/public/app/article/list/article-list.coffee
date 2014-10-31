angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  articleFormat = (data) ->
    obj = []
    for post in data.value
      date = post.date?.format('yyyy-MM')
      obj[date]=[]  unless obj[date]
      obj[date].push(post)
    { date:key, posts:value } for key, value of obj when obj.hasOwnProperty key

  $routeProvider
    .when "/",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      title: 'Article list'
      resolve:
        articles: ['$route', '$q', 'Articles', 'Categories', ($route, $q, Articles, Categories) ->
          deferred = $q.defer()
          Categories.main (category) ->
            Articles.query
              $filter: "category eq '#{category.value[0].name}'"
              $top: 10000
              $count: true
              $select: 'title,url,meta,description,date,category,tag'
            ,(data)->
              deferred.resolve articleFormat(data)
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
            $top: 10000
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          ,(data)->
            deferred.resolve articleFormat(data)
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
            $top: 10000
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          ,(data)->
            deferred.resolve articleFormat(data)
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
            $top: 10000
            $skip: ($route.current.params.p || 1) * 10 - 10
            $count: true
            $select: 'title,url,meta,description,date,category,tag'
          , (data)->
            deferred.resolve articleFormat(data)
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

  $scope.category = $routeParams.category || articles[0].posts[0].category

  #Turn page
  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.edit = (item) ->
    $window.location.href="/admin/article/#{item._id}"
])
