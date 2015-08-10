angular.module("framework.controllers.main",[])

.controller('MainCtrl', ["$scope","$rootScope","$http","$location","version", "context", "$localStorage", 'ngDialog'
($scope,$rootScope,$http,$location, version, context, $localStorage, ngDialog) ->
  $scope.$on "loginSuccessed", ->
    version.get().then (data)->
      return if !data.length
      $scope.newVersion = data[0]
      if $scope.newVersion.ver != $localStorage.ver
        dialog = ngDialog.open
          template: '/app/dialogs/version-upgrade/version-upgrade-dialog.tpl.html'
          controller: 'VersionUpgradeDialogCtrl'
          scope: $scope
        dialog.closePromise.then ->
          $localStorage.ver = $scope.newVersion.ver

    $rootScope.account = context.account

    $rootScope.__login = true
    $rootScope.__logoff = false

    #Back to page.
    $location.path($scope.__returnUrl || '/').replace()
    $scope.__returnUrl = null

  $scope.$on "logoutSuccessed", ->
    $rootScope.__login = false
    $rootScope.__logoff = true
])

