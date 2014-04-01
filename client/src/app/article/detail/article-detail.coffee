angular.module('article-detail',['resource.articles','resource.comments'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/post/:url",
      templateUrl: "/app/article/detail/article-detail.tpl.html"
      controller: 'ArticleDetailCtrl'
      resolve:
        article: ['$route','$q','Article',($route,$q,Article)->
          deferred = $q.defer()
          Article.getOnce
            $filter:"Url eq '#{$route.current.params.url}' and IsDeleted eq false"
            $expand:'Tags,Group/Channel,Comments'
          , (data) -> deferred.resolve data.value[0]
          deferred.promise
        ]
])

.controller('ArticleDetailCtrl',
["$scope","$rootScope","$window","$translate","$routeParams","progressbar","Article","Comment","article","context", "messager"
($scope,$rootScope,$window,$translate,$routeParams,progressbar,Article,Comment,article,context, messager) ->
  $window.scroll(0,0)

  $scope.item=article
  if !$scope.item
    $rootScope.title='404'
    return
  $rootScope.title=$scope.item.Title
  $scope.$emit("ChannelChange",$scope.item.Group.Channel)
  codeformat()#格式化代码
  #上一篇
  $scope.prevPost = Article.getOnce
    $select:'Url,Title'
    $filter:"IsDeleted eq false and CreateDate lt datetime'#{$scope.item.CreateDate}' and Group/Url eq '#{$scope.item.Group.Url}'"
    $orderby:'CreateDate desc' 
  #下一篇
  $scope.nextPost = Article.getOnce
    $select:'Url,Title'
    $filter:"IsDeleted eq false and CreateDate gt datetime'#{$scope.item.CreateDate}' and Group/Url eq '#{$scope.item.Group.Url}'"
    $orderby:'CreateDate'
  #相关文章
  if $scope.item.Tags.length
    relatedFilter=''
    for tag,i in $scope.item.Tags
      relatedFilter+=" or Tags/any(tag#{i}:tag#{i}/Name eq '#{tag.Name}')"
    relatedFilter=relatedFilter.substring(4)
    relatedFilter="IsDeleted eq false and PostId ne (guid'#{$scope.item.PostId}') and (#{relatedFilter})"
    $scope.relatedPost = Article.getOnce
      $top:8
      $select:'Url,Title,PubDate'
      $filter:relatedFilter
      $orderby:'CreateDate desc' 
  #评论
  Comment.get
    $filter:"PostId eq (guid'#{$scope.item.PostId}') and IsDeleted eq false"
  , (data) ->
    $scope.item.Comments=data.value
    for item in $scope.item.Comments
      if !item.Avatar
        if item.Email
          item.Avatar='http://www.gravatar.com/avatar/' + md5(item.Email)
        else
          item.Avatar='/img/avatar.png'
  #浏览量+1
  Article.browsed id:"(guid'#{$scope.item.PostId}')"

  $scope.entity=
    Author:context.account.name
    Email:context.account.email
    Url:context.account.url
  $scope.editmode=!context.account.name
  $scope.isAdmin = context.auth.admin

  $scope.del = (item) ->
    messager.confirm ->
      Comment.del {id:item.CommentId}
      ,(data)->
        messager.success "“##{item.Content}”  be moved to trash."
        item.IsDeleted = true
      ,(error)->
        messager.error error.data.ExceptionMessage ? error.status

  $scope.save = () ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    progressbar.start()
    $scope.loading = $translate("global.post")
    $scope.entity.CommentId=UUID.generate()
    $scope.entity.Post=PostId:$scope.item.PostId
    Comment.save $scope.entity
    ,(data)->
      $scope.item.Comments.push(data)
      $scope.entity.Content=""
      Article.commented id:"(guid'#{$scope.item.PostId}')"
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
      context.account =
        name: $scope.entity.Author
        email: $scope.entity.Email
        url: $scope.entity.Url
    ,(error)->
      $scope.submitted=false
      $scope.loading = ""

  $scope.remove = (item) ->
    messager.confirm ->
      Comment.remove id:"(guid'#{item.CommentId}')",->
        item.IsDeleted=true
        messager.success "Comment has been removed."

  $scope.edit = (item) ->
    $window.location.href="/admin/article('#{item.PostId}')"
])