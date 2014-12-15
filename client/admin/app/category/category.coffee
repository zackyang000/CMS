angular.module('category',['resource.categories', 'category-edit'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/category",
      templateUrl: "/app/category/category.tpl.html"
      controller: 'CategoryCtrl'
])

.controller('CategoryCtrl',
["$scope", "ngDialog", "Categories", "messager"
($scope, ngDialog, Categories, messager) ->
  $scope.languages = config.languages

  load = ->
    Categories.query (data)->
      $scope.list = data.value

  $scope.openAddDialog = ()->
    $scope.entity = {}
    openDialog()

  $scope.openEditDialog = (item) ->
    $scope.entity = item
    openDialog()

  openDialog = ->
    dialog = ngDialog.open
      template: '/app/category/category-edit-dialog.tpl.html'
      controller: 'CategoryEditDialogCtrl'
      scope: $scope
    dialog.closePromise.then load

  $scope.remove = (item)->
    messager.confirm ->
      tip.show("Deleting")
      Categories.delete {id:item._id}
      , (data) ->
        messager.success "Delete channel successfully."
        load()

  load()
])
