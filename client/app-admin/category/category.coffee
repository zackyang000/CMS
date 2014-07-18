angular.module('category',['resource.categories'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/category",
      templateUrl: "/app-admin/category/category.tpl.html"
      controller: 'CategoryCtrl'
])

.controller('CategoryCtrl',
["$scope","$dialog","Categories", "messager"
($scope,$dialog,Categories, messager) ->
  $scope.entity = {}

  load = ->
    $scope.loading="Loading"
    Categories.query (data)->
      $scope.list = data
      $scope.loading=""

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy(item)
    $scope.editDialog = true

  $scope.save = ->
    $scope.loading="Saving"
    if $scope.entity._id
      Categories.update {id:$scope.entity._id}, $scope.entity
      ,(data)->
        messager.success "Edit category successfully."
        $scope.close()
        load()
    else
      Categories.save $scope.entity
      ,(data)->
        messager.success "Add category successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    messager.confirm ->
      $scope.loading="Deleting"
      item.IsDeleted = true
      Categories.edit {id:$scope.entity._id},item
      ,(data)->
        messager.success "Delete channel successfully."
        load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])