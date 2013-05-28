
BoardController=["$scope", "$http","Message", ($scope, $http,Message) ->
      $scope.entity= {};
      $scope.isAdmin=false
      $scope.loading=true

      $http.get("/comment/UserInfo").success (data) ->
        $scope.entity.Author = data.Author
        $scope.entity.Email = data.Email
        $scope.entity.Url = data.Url
        $scope.AuthorForDisplay=data.Author
        $scope.editmode=data.Author==''

      $scope.list = Message.query ->
        $scope.loading = false 
      
      $scope.del = (item) ->
        $http.post("/board/delete", "{id:'#{item.BoardId}'}")
          .success (data) ->
            if data.result
              message.success "“##{item.Content}”  be moved to trash."
              item.IsDeleted = true
            else
              message.error data.reason

      $scope.renew = (item) ->
        $http.post("/board/renew", "{id:'#{item.BoardId}'}")
          .success (data) ->
            if data.result
              message.success "“##{item.Content}”  be moved to trash."
              item.IsDeleted = false
            else
              message.error data.reason

      $scope.save = () ->
        $scope.submitting=true
        $http.post("/board/add", $scope.entity)
          .success (data) ->
            if data.result
              message.success "Message has been submitted."
              $scope.list.unshift(data.model)
              $scope.entity.Content=""
              $scope.AuthorForDisplay=data.model.Author
              $scope.editmode=false
              angular.resetForm($scope, 'form')
            else
              message.error data.reason
            $scope.submitting=false
    ]