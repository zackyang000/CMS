angular.module('article-detail',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/post/:url",
      templateUrl: "/app/article/detail/article-detail.tpl.html"
      controller: 'ArticleDetailCtrl'
      resolve:
        article: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.list
            $filter: "url eq '#{$route.current.params.url}'"
          , (data) ->
            deferred.resolve(data.value[0])
          deferred.promise
        ]
])

.controller('ArticleDetailCtrl',
["$scope", "$rootScope", "$window", "$translate", "$route", "dataCacheCategories", "article", "Articles", "context", "ngProgress", "messager"
($scope, $rootScope, $window, $translate, $route, dataCacheCategories, article, Articles, context, ngProgress, messager) ->
  $window.scroll(0, 0)

  $scope.item = article

  if !$scope.item
    $rootScope.title = '404'
    return

  $rootScope.title = $scope.item.title

  for category in dataCacheCategories
    if article.category == category.url
      $scope.category = category
      break

  $rootScope.$broadcast("categoryChange", article.category)

  codeformat()#格式化代码

  #初始化新评论
  $scope.entity =
    author :
      name : context.account.name
      email : context.account.email
  $scope.editmode = !context.account.name
  $scope.auth = context.auth

  #提交评论
  $scope.save = ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    ngProgress.start()
    $scope.loading = $translate("global.post")
    $scope.entity.id = $scope.item.id
    $scope.entity.date = new Date()
    Articles.addComment $scope.entity
    , (data)->
      $scope.item.comments.push(data)
      $scope.entity.content = ""
      ngProgress.complete()
      $scope.submitted=false
      $scope.loading = ""
      context.account =
        name: $scope.entity.author.name
        email: $scope.entity.author.email
        url: $scope.entity.url
    , (error)->
      ngProgress.complete()
      $scope.submitted=false
      $scope.loading = ""

  $scope.edit = (item) ->
    $window.location.href = "#{config.url.admin}/article/#{item.id}"

  $scope.remove = (item, index) ->
    messager.confirm ->
      $scope.item.comments.splice(index, 1)
      $scope.item.meta.comments--
      Articles.put {id: $scope.item.id}, $scope.item, (item) ->
        messager.success "Comment has been removed."
        $scope.item = item
])
