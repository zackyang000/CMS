LoginController=["$scope","User","$window",($scope,User,$window) ->
  $scope.login = ->
    $scope.submitting=true
    User.login {id:'(1)'},$scope.user
      ,(data)->
          $scope.submitting=false
          $window.location.href='/admin'
      ,(error)->
          message.error error.data['odata.error'].innererror.message
          $scope.submitting=false
]