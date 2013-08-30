ArticleDetailController=["$scope","$routeParams","progressbar","Article","Comment",
($scope,$routeParams,progressbar,Article,Comment) ->
  $scope.$parent.showBanner=false
  $scope.loading=true

  $scope.url =$routeParams.url
  Article.get
    $filter:"Url eq '#{$scope.url}'"
   , (data)->
    $scope.loading=false
    $scope.item=data.value[0]
    if !$scope.item
      $scope.$parent.title='404'
      return
    $scope.$parent.title=$scope.item.Title
    codeformat()#格式化代码
    $scope.entity.PostId = $scope.item.PostId
    #上一篇
    $scope.prevPost = Article.nav
      $filter:"IsDeleted eq false and CreateDate lt datetime'#{$scope.item.CreateDate}' and Group/Url eq '#{$scope.item.Group.Url}'"
      $orderby:'CreateDate desc' 
    #下一篇
    $scope.nextPost = Article.nav
      $filter:"IsDeleted eq false and CreateDate gt datetime'#{$scope.item.CreateDate}' and Group/Url eq '#{$scope.item.Group.Url}'"
      $orderby:'CreateDate'
    #相关文章
    if $scope.item.Tags.length
      relatedFilter=''
      for tag,i in $scope.item.Tags
        relatedFilter+=" or Tags/any(tag#{i}:tag#{i}/Name eq '#{tag.Name}')"
      relatedFilter=relatedFilter.substring(4)
      relatedFilter="IsDeleted eq false and PostId ne (guid'#{$scope.item.PostId}') and (#{relatedFilter})"
      $scope.relatedPost = Article.related
        $filter:relatedFilter
        $orderby:'CreateDate desc' 
    #评论
    for item in $scope.item.Comments
      if item.Email
        item.Gravatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
      else
        item.Gravatar='/Content/img/avatar.png'
    Article.browsed id:"(guid'#{$scope.item.PostId}')"

  $scope.entity= {}

  $scope.$watch 'User',->
    if $scope.User
      $scope.entity.Author = $scope.User.UserName
      $scope.entity.Email = $scope.User.Email
      $scope.entity.Url = $scope.User.Url
      $scope.AuthorForDisplay=$scope.User.UserName
      $scope.editmode=$scope.User.UserName=='' or not $scope.User.UserName?
      
  $scope.del = (item) ->
    Comment.del {id:item.CommentId}
    ,(data)->
      message.success "“##{item.Content}”  be moved to trash."
      item.IsDeleted = true
    ,(error)->
      message.error error.data.ExceptionMessage ? error.status

  $scope.renew = (item) ->
    Comment.renew {id:item.CommentId}
    ,(data)->
      message.success "“##{item.Content}”  be renew."
      item.IsDeleted = false
    ,(error)->
      message.error error.data.ExceptionMessage ? error.status

  $scope.save = () ->
    progressbar.start()
    $scope.submitting=true
    $scope.entity.CommentId=UUID.generate()
    Comment.save $scope.entity
    ,(data)->
      message.success "Comment has been submitted."
      $scope.item.Comments.push(data)
      $scope.entity.Content=""
      $scope.AuthorForDisplay=data.Author
      $scope.editmode=false
      angular.resetForm($scope, 'form')
      $scope.submitting=false
      Article.commented id:"(guid'#{$scope.item.PostId}')"
      progressbar.complete()
    ,(error)->
      message.error error.data.ExceptionMessage ? error.status

  $scope.remove = (item) ->
    Comment.remove id:"(guid'#{item.CommentId}')",->
      item.IsDeleted=true
      message.success "Comment has been removed."
]