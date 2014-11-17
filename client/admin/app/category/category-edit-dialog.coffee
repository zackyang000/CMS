angular.module('category-edit',[])

.controller('CategoryEditDialogCtrl',
["$scope","Categories", "messager", "tip"
($scope, Categories, messager, tip) ->
  $scope.entity = angular.copy $scope.$parent.entity

  $scope.save = ->
    tip.show("Saving")
    if $scope.entity._id
      Categories.update {id:$scope.entity._id}, $scope.entity
      ,(data)->
        messager.success "Edit category successfully."
        $scope.closeThisDialog()
    else
      Categories.save $scope.entity
      ,(data)->
        messager.success "Add category successfully."
        $scope.closeThisDialog()

  $scope.close = ->
    $scope.closeThisDialog()
])
