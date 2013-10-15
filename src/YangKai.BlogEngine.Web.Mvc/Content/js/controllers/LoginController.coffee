LoginController=["$scope","$window","User",($scope,$window,User) ->

  $scope.opts=
    dialogFade:true
    backdropFade:true
    templateUrl: '/partials/admin/login-dialog.html'
    controller: 'LoginDialogController'

  $scope.open = ->
    $window.location.href='/admin/'

  $scope.signin = ->
    $scope.submitting=true
    User.signin {id:'(1)'},$scope.user
      ,(data)->
        $scope.submitting=false
        $window.location.href='/admin/'
      ,(error)->
        $scope.error=error.data['odata.error'].innererror.message
        $scope.user.Password=''
        $scope.submitting=false

  $scope.signout = ->
    $scope.submitting=true
    User.signout {id:'(1)'}
      ,(data)->
        $scope.submitting=false
        $window.location.href='/'

  $scope.manage = ->
    $window.location.href='/admin/'

  $scope.view = ->
    $window.location.href='/'
]
