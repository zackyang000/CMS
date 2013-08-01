MessageController=["$scope","Message", ($scope,Message) ->
      $scope.$parent.title='留言板'
      $scope.$parent.showBanner=false
      $scope.loading=true
      $scope.entity= {}

      $scope.entity.Author = $scope.Name
      $scope.entity.Email = $scope.Email
      $scope.entity.Url = $scope.Url
      $scope.AuthorForDisplay=$scope.Name
      $scope.editmode=$scope.Name=='' or not $scope.Name?
      $scope.list = Message.query ->
        for item in $scope.list
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
        Message.add $scope.entity
        ,(data)->
          message.success "Message has been submitted."
          $scope.list.unshift(data)
          $scope.entity.Content=""
          $scope.AuthorForDisplay=data.Author
          $scope.editmode=false
          angular.resetForm($scope, 'form')
          $scope.submitting=false
        ,(error)->
          message.error error.data.ExceptionMessage ? error.status
    ]