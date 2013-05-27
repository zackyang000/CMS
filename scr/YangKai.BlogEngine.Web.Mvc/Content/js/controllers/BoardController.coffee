
BoardController=["$scope", "$http", ($scope, $http) ->
      $scope.loading = true
      #$scope.list = Message.query() 无效??? 
      $http.get("/board/list").success (data) ->
        $scope.list = data
        $scope.loading = false
      
      $scope.del = (entity) ->
        $http.post("/board/delete", "{id:'#{entity.BoardId}'}")
          .success (data) ->
            if data.result
              message.success "“##{entity.Content}”  be moved to trash."
              entity.IsDeleted = true
            else
              message.error data.reason

      $scope.renew = (entity) ->
        $http.post("/board/renew", "{id:'#{entity.BoardId}'}")
          .success (data) ->
            if data.result
              message.success "“##{entity.Content}”  be moved to trash."
              entity.IsDeleted = false
            else
              message.error data.reason
    ]