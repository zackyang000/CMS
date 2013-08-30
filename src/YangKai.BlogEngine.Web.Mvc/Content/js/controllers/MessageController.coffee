MessageController=["$scope","progressbar","Message", 
($scope,progressbar,Message) ->
  $scope.$parent.title='Message Boards'
  $scope.$parent.showBanner=false
  $scope.loading=true
  $scope.entity= {}

  $scope.$watch 'User',->
    if $scope.User
      $scope.entity.Author = $scope.User.UserName
      $scope.entity.Email = $scope.User.Email
      $scope.entity.Url = $scope.User.Url
      $scope.AuthorForDisplay=$scope.User.UserName
      $scope.editmode=$scope.User.UserName=='' or not $scope.User.UserName?
      
  $scope.list = Message.query $filter:'IsDeleted eq false',->
    for item in $scope.list.value
      if item.Email
        item.Gravatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
      else
        item.Gravatar='/Content/img/avatar.png'
    $scope.loading = false 
      
  $scope.save = () ->
    progressbar.start()
    $scope.submitting=true
    $scope.entity.BoardId=UUID.generate()
    Message.save $scope.entity
    ,(data)->
      message.success "Message has been submitted."
      $scope.list.value.unshift(data)
      $scope.entity.Content=""
      $scope.AuthorForDisplay=data.Author
      $scope.editmode=false
      angular.resetForm($scope, 'form')
      $scope.submitting=false
      progressbar.complete()

  $scope.remove = (item) ->
    Message.remove id:"(guid'#{item.BoardId}')",->
      item.IsDeleted=true
      message.success "Message has been removed."
  ]