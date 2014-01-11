angular.module('article-list',['resource.articles'])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/list/:channel/:group/:type/:query",
    templateUrl: "/Content/app/article/list/article-list.tpl.html"
    controller: 'ArticleListCtrl')
  .when("/list/:channel/:group",
    templateUrl: "/Content/app/article/list/article-list.tpl.html"
    controller: 'ArticleListCtrl')
  .when("/list/:channel",
    templateUrl: "/Content/app/article/list/article-list.tpl.html"
    controller: 'ArticleListCtrl'
    resolve:
      articles: ['$route','$q','Article',($route,$q,Article)->
        deferred = $q.defer()
        filter='IsDeleted eq false'
        Article.queryOnce
          $filter:"IsDeleted eq false and Group/Channel/Url eq '#{$route.current.params.channel}'"
          $skip:($route.current.params.p ? 1)*10 - 10
        , (data)->
          deferred.resolve data
        deferred.promise
      ]
  )
  .when("/search/:key",
    templateUrl: "/Content/app/article/list/article-list.tpl.html"
    controller: 'ArticleListCtrl')
])

.controller('ArticleListCtrl',
["$scope","$window","$translate","$routeParams","$location","Article","articles"
($scope,$window,$translate,$routeParams,$location,Article,articles) ->
  $scope.$parent.showBanner=false

  $scope.$parent.title=$routeParams.group ? $routeParams.channel ? "Search Result '#{$scope.key}'"
  $scope.currentPage =$routeParams.p ? 1

  $scope.params=
    channel:$routeParams.channel
    group:$routeParams.group
    key:$routeParams.key
    tag:if $routeParams.type=='tag' then $routeParams.query else ''

  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

    #filter='IsDeleted eq false'
    #filter+=" and Group/Channel/Url eq '#{$scope.params.channel}'" if $scope.params.channel
    #filter+=" and Group/Url eq '#{$scope.params.group}'" if $scope.params.group
    #filter+=" and indexof(Title, '#{$scope.params.key}') gt -1" if $scope.params.key
    #filter+=" and Tags/any(tag:tag/Name eq '#{$scope.params.tag}')" if $scope.params.tag
  scroll(0,0)
  $scope.list = articles

  $scope.edit = (item) ->
    $window.location.href="/admin/article('#{item.PostId}')"
])