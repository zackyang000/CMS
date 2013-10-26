angular.module('admin-basedata-category',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/channel(':channel')/group(':group')/category",
    templateUrl: "/content/app/admin/basedata/category/basedata-category.tpl.html"
    controller: 'CategoryCtrl')
])

.controller('CategoryCtrl',
["$scope","$dialog","$routeParams","Category","Group", 
($scope,$dialog,$routeParams,Category,Group) ->
  Group.query
    $filter:"Url eq '#{$routeParams.group}'"
    $expand:'Channel'
    ,(data)->
      $scope.group=data.value[0]

  load = ->
    $scope.loading="Loading"
    Category.query 
      $filter:"IsDeleted eq false and Group/Url eq '#{$routeParams.group}'"
      ,(data)->
        $scope.list=data
        $scope.loading=""

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy(item)
    $scope.editDialog = true

  $scope.save = ->
    $scope.loading="Saving"
    $scope.entity.Group=$scope.group
    if $scope.entity.CategoryId
      Category.edit {id:"(guid'#{$scope.entity.CategoryId}')"},$scope.entity
      ,(data)->
        message.success "Edit category successfully."
        $scope.close()
        load()
    else
      $scope.entity.CategoryId=UUID.generate()
      Category.save $scope.entity
      ,(data)->
        message.success "Add category successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    $scope.loading="Deleting"
    item.Group=$scope.group
    message.confirm ->
      item.IsDeleted=true
      Category.edit {id:"(guid'#{item.CategoryId}')"},item
      ,(data)->
          message.success "Delete category successfully."
          load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])