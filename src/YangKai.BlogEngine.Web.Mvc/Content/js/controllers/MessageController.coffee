MessageController=["$scope","Message", ($scope,Message) ->
    $scope.$parent.title='留言板'
    $scope.$parent.showBanner=false
    $scope.loading=true
    $scope.entity= {}

    $scope.entity.Author = $scope.User.UserName
    $scope.entity.Email = $scope.User.Email
    $scope.entity.Url = $scope.User.Url
    $scope.AuthorForDisplay=$scope.User.UserName
    $scope.editmode=$scope.User.UserName=='' or not $scope.User.UserName?
    $scope.list = Message.query ->
      for item in $scope.list.value
        if item.Email
          item.Gravatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
        else
          item.Gravatar='/Content/img/avatar.png'
      $scope.loading = false 
      
    $scope.del = (item) ->
      Message.del {id:item.BoardId}
      ,(data)->
        message.success "“##{item.Content}”  be moved to trash."
        item.IsDeleted = true
      ,(error)->
        message.error error.data.ExceptionMessage ? error.status

    $scope.renew = (item) ->
      Message.renew {id:item.BoardId}
      ,(data)->
        message.success "“##{item.Content}”  be renew."
        item.IsDeleted = false
      ,(error)->
        message.error error.data.ExceptionMessage ? error.status

    $scope.save = () ->
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
      ,(error)->
        message.error error.data.ExceptionMessage ? error.status
  ]