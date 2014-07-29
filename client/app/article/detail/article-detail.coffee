angular.module('article-detail',['resource.articles','resource.comments'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/post/:url",
      templateUrl: "/app/article/detail/article-detail.tpl.html"
      controller: 'ArticleDetailCtrl'
      resolve:
        article: ['$route','$q','Articles',($route,$q,Articles)->
          deferred = $q.defer()
          Articles.query
            url: $route.current.params.url
          , (data) ->
            deferred.resolve(data[0])
          deferred.promise
        ]
])

.controller('ArticleDetailCtrl',
["$scope","$window", "$translate", "$route", "article", "Comments", "context", "progressbar"
($scope, $window, $translate, $route, article, Comments, context, progressbar) ->
  $window.scroll(0,0)
  debugger
  $scope.item = article

  codeformat()#格式化代码

  #获取评论
  Comments.query
    id: 'article/' + $route.current.params.url
  , (data) ->
    $scope.comments = data

  #初始化新评论
  $scope.entity=
    type : 'article'
    author : context.account.name
    email : context.account.email
    url : context.account.url
    linkId : $route.current.params.url
  $scope.editmode = !context.account.name
  $scope.isAdmin = context.auth.admin

  #提交评论
  $scope.save = ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    progressbar.start()
    $scope.loading = $translate("global.post")
    Comments.save $scope.entity
    , (data)->
      $scope.comments.push(data)
      $scope.entity.content = ""
      #Article.commented id:"(guid'#{$scope.item.PostId}')"
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
      context.account =
        name: $scope.entity.author
        email: $scope.entity.email
        url: $scope.entity.url
    ,(error)->
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""

  #浏览量+1
  #Article.browsed id:"(guid'#{$scope.item.PostId}')"

  $scope.edit = (item) ->
    $window.location.href="/admin/article/#{item._id}"
])