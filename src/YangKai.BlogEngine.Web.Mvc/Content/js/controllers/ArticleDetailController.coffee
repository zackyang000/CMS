ArticleDetailController=["$scope","$routeParams","Article","Comment",
($scope,$routeParams,Article,Comment) ->
  $scope.$parent.showBanner=false
  $scope.loading=true

  $scope.url =$routeParams.url
  Article.get
    $filter:"Url eq '#{$scope.url}'"
   , (data)->
    $scope.item=data.value[0]
    $scope.$parent.title=$scope.item.Title
    codeformat()#格式化代码
    $scope.loading=false
    $scope.entity.PostId = $scope.item.PostId
    #上一篇 & 下一篇
    $scope.nav = Article.nav
      id:$scope.item.PostId
    #相关文章
    $scope.related = Article.related
      id:$scope.item.PostId
    #评论
      for item in $scope.item.Comments
        if item.Email
          item.Gravatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
        else
          item.Gravatar='/Content/img/avatar.png'


  $scope.entity= {}

  #TODO 如果Global中用户信息读取比此处慢,将无法绑定用户信息到界面.
  $scope.entity.Author = $scope.Name
  $scope.entity.Email = $scope.Email
  $scope.entity.Url = $scope.Url
  $scope.AuthorForDisplay=$scope.Name
  $scope.editmode=$scope.Name=='' or not $scope.Name?
      
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
    $scope.submitting=true
    Comment.save $scope.entity
    ,(data)->
      message.success "Comment has been submitted."
      $scope.item.Comments.push(data)
      $scope.entity.Content=""
      $scope.AuthorForDisplay=data.Author
      $scope.editmode=false
      angular.resetForm($scope, 'form')
      $scope.submitting=false
    ,(error)->
      message.error error.data.ExceptionMessage ? error.status
]