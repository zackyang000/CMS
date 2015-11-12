angular.module('version-upgrade-dialog',[])

.controller('VersionUpgradeDialogCtrl',
["$scope", '$location', ($scope, $location) ->
  $scope.more = ->
    $location.path '/system/history'
    $scope.close()

  $scope.close = ->
    $scope.closeThisDialog()
])
