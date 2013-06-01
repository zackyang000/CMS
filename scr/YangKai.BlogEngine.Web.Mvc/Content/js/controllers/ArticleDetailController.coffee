ArticleDetailController=["$scope","$routeParams","Article","Comment",
($scope,$routeParams,Article,Comment) ->
  $scope.$parent.showBanner=false
  $scope.$parent.loading=true

  $scope.url =$routeParams.url
  $scope.item = Article.get
    id:$scope.url
  , ->
    $scope.$parent.loading=false
    codeformat()#格式化代码
    $scope.entity.PostId = $scope.item.PostId
    $scope.nav = Article.nav
      id:$scope.item.PostId
    $scope.list = Comment.query
      PostId:$scope.item.PostId

  $scope.entity= {}

  $scope.entity.Author = $scope.Name
  $scope.entity.Email = $scope.Email
  $scope.entity.Url = $scope.Url
  $scope.AuthorForDisplay=$scope.Name
  $scope.editmode=$scope.Name=='' or not $scope.Name?
      
  $scope.del = (item) ->
    debugger
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
    Comment.add $scope.entity
    ,(data)->
      message.success "Message has been submitted."
      $scope.list.push(data)
      $scope.entity.Content=""
      $scope.AuthorForDisplay=data.Author
      $scope.editmode=false
      angular.resetForm($scope, 'form')
      $scope.submitting=false
    ,(error)->
      message.error error.data.ExceptionMessage ? error.status
]