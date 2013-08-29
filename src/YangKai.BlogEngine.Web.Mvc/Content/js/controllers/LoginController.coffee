LoginController=["$scope","$window","$dialog","User",($scope,$window,$dialog,User) ->

  $scope.opts=
    dialogFade:true
    backdropFade:true
    templateUrl: '/partials/admin/login-dialog.html'
    controller: 'LoginDialogController'

  $scope.open = ->
    $dialog.dialog($scope.opts).open()

  $scope.close = ->
    $scope.sgindialog = false

  $scope.signin = ->
    $scope.submitting=true
    User.signin {id:'(1)'},$scope.user
      ,(data)->
        $scope.submitting=false
        $window.location.href='/admin'
      ,(error)->
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

LoginDialogController=["$scope","$window", "dialog","User",($scope,$window, dialog,User) ->
  $scope.close = (result) ->
    dialog.close(result)

  $scope.signin = ->
    $scope.submitting=true
    User.signin {id:'(1)'},$scope.user
      ,(data)->
        $scope.submitting=false
        $window.location.href='/'
      ,(error)->
        $scope.user.Password=''
        $scope.submitting=false
]