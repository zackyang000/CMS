angular.module('article-detail',['resource.articles','resource.comments'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/post/:url",
      templateUrl: "/app/article/detail/article-detail.tpl.html"
      controller: 'ArticleDetailCtrl'
      resolve:
        article: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Articles.get
            id: $route.current.params.url
          , (data) ->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('ArticleDetailCtrl',
["$scope","$window","article", "$route", "Comments"
($scope, $window, article, $route ,Comments) ->
  $window.scroll(0,0)

  $scope.item = article

  codeformat()#格式化代码

  #获取评论
  Comments.get
    id: $route.current.params.url
  , (data) ->
    $scope.comments = data

  #浏览量+1
  #Article.browsed id:"(guid'#{$scope.item.PostId}')"

  $scope.edit = (item) ->
    $window.location.href="/admin/article('#{item.url}')"
])