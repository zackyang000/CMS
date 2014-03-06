angular.module('admin-basedata-channel',['resource.channels'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/admin/channel",
      templateUrl: "/content/app-admin/basedata/channel/basedata-channel.tpl.html"
      controller: 'ChannelCtrl'
])

.controller('ChannelCtrl',
["$scope","$dialog","Channel", 
($scope,$dialog,Channel) ->
  $scope.entity = {}

  load = ->
    $scope.loading="Loading"
    Channel.query (data)->
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
      $scope.loading="Deleting"
      item.IsDeleted = true
      Channel.edit {id:"(guid'#{item.ChannelId}')"},item
      ,(data)->
        message.success "Delete channel successfully."
        load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])