angular.module('admin-basedata-group',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/channel(':channel')/group",
    templateUrl: "/content/app/admin/basedata/group/basedata-group.tpl.html"
    controller: 'GroupCtrl')
])

.controller('GroupCtrl',
["$scope","$dialog","$routeParams","Group","Channel", 
($scope,$dialog,$routeParams,Group,Channel) ->
  Channel.query
    $filter:"Url eq '#{$routeParams.channel}'"
    ,(data)->
      $scope.channel=data.value[0]
  $scope.entity = {}

  load = ->
    $scope.loading="Loading"
    Group.query 
      $filter:"IsDeleted eq false and Channel/Url eq '#{$routeParams.channel}'"
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
    $scope.entity.Channel=$scope.channel
    if $scope.entity.GroupId
      Group.edit {id:"(guid'#{$scope.entity.GroupId}')"},$scope.entity
      ,(data)->
        message.success "Edit group successfully."
        $scope.close()
        load()
    else
      $scope.entity.GroupId=UUID.generate()
      Group.save $scope.entity
      ,(data)->
        message.success "Add group successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    $scope.loading="Deleting"
    item.Channel=$scope.channel
    message.confirm ->
      item.IsDeleted=true
      Group.edit {id:"(guid'#{item.GroupId}')"},item
      ,(data)->
          message.success "Delete group successfully."
          load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])