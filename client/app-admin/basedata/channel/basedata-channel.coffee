angular.module('basedata-channel',['resource.categories'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/channel",
      templateUrl: "/app-admin/basedata/channel/basedata-channel.tpl.html"
      controller: 'ChannelCtrl'
])

.controller('ChannelCtrl',
["$scope","$dialog","Channel", "messager"
($scope,$dialog,Channel, messager) ->
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
        messager.success "Edit channel successfully."
        $scope.close()
        load()
    else
      $scope.entity.ChannelId=UUID.generate()
      Channel.save $scope.entity
      ,(data)->
        messager.success "Add channel successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    messager.confirm ->
      $scope.loading="Deleting"
      item.IsDeleted = true
      Channel.edit {id:"(guid'#{item.ChannelId}')"},item
      ,(data)->
        messager.success "Delete channel successfully."
        load()

  $scope.close = ->
    $scope.editDialog = false

  load()
])