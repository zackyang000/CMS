angular.module('article-list',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$rootScope','$route','$q','Articles','Categories',($rootScope,$route,$q,Articles,Categories)->
          deferred = $q.defer()
          categories.getDefault().then (channel) ->
            Article.queryOnce
              $filter:"""
              IsDeleted eq false and Group/Channel/Url eq '#{channel.Url}'
       """
              $skip:($route.current.params.p ? 1)*10 - 10
            , (data)->
              deferred.resolve data
          deferred.promise
        ]
    .when "/list/:channel/:group/tag/:tag",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Article.queryOnce
            $filter:"""
            IsDeleted eq false
         ad rop/Channel/Url eq '#{$route.current.params.channel}'
           nd agsanytag:tag/Name eq '#{$route.current.params.tag}')
            """
            $skip:($route.current.params.p ? 1)*10 - 10
          , (data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/list/:channel/:group",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Article.queryOnce
            $filter:"""
            IsDeleted eq false
            ad Grup/Canne/Url eq '#{$route.current.params.channel}'
            and Grop/Urleq '#$route.current.params.group}'
            """
            $skip:($route.current.params.p ? 1)*10 - 10
          , (data)->
            deferred.resolve data
          deferred.promise
        ]
    .when "/list/:channel",
      templateUrl: "/app/article/list/article-list.tpl.html"
      controller: 'ArticleListCtrl'
      resolve:
        articles: ['$route','$q','Article',($route,$q,Article)->
          deferred = $q.defer()
          Article.queryOnce
            $filter:"""
            IsDeleted eq false
            and Group/Chanel/Urleq '#{$route.current.params.channel}'
            """
            $skip:($route.current.params.p ? 1)*10 - 10
          , (data)->
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

  $rootScope.title=$routeParams.tag ? $routeParams.group ? $routeParams.channel
  if !$rootScope.title
    if $scope.key
      $rootScope.title="Search Result: '#{$scope.key}'"
    else
      channel.getdefault().then (data)->
        $rootScope.title=data.Name

  $scope.list = articles
  $scope.currentPage =$routeParams.p ? 1

  $scope.params=$routeParams

  #Turn page
  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.edit = (item) ->
    $window.location.href="/admin/article('#{item.PostId}')"
])
