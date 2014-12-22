1
angular.module('user-edit',[])

.controller('UserEditDialogCtrl',
['$scope', 'Users', 'messager', 'tip'
($scope, Users, messager, tip) ->
  $scope.entity = angular.copy $scope.$parent.entity

  $scope.save = ->
    tip.show("Saving")
    if $scope.entity._id
      Users.update {id:$scope.entity._id}, $scope.entity
      ,(data)->
        messager.success "Edit user successfully."
        $scope.closeThisDialog()
    else
      Users.save $scope.entity
      ,(data)->
        messager.success "Add user successfully."
        $scope.closeThisDialog()

  $scope.close = ->
    $scope.closeThisDialog()
])
