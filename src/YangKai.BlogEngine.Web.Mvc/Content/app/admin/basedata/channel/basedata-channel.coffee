angular.module('admin-basedata-channel',[])

.controller('ChannelCtrl',
["$scope","$dialog","Channel", 
($scope,$dialog,Channel) ->
  $scope.entity = {}

  load = ->
    Channel.query (data)->
      $scope.list = data

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy(item)
    $scope.editDialog = true

  $scope.save = ->
    if $scope.entity.ChannelId
      Channel.edit {id:"(guid'#{$scope.entity.ChannelId}')"},$scope.entity
      ,(data)->
        message.success "Edit channel successfully."
        $scope.close()
        load()
    else
      $scope.entity.ChannelId=UUID.generate()
      Channel.save $scope.entity
      ,(data)->
        message.success "Add channel successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    message.confirm ->
      item.IsDeleted = true
      Channel.edit {id:"(guid'#{item.ChannelId}')"},item
      ,(data)->
        message.success "Delete channel successfully."
        load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])