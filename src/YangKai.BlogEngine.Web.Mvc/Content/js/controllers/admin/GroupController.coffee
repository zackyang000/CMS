GroupController=["$scope","$dialog","$routeParams","Group","Channel", 
($scope,$dialog,$routeParams,Group,Channel) ->
  $scope.loading=true

  Channel.query
    $filter:"Url eq '#{$routeParams.channel}'"
    ,(data)->
      $scope.channel=data.value[0]

  $scope.entity = {}

  load = ->
    Group.query 
      $filter:"IsDeleted eq false and Channel/Url eq '#{$routeParams.channel}'"
      ,(data)->
        $scope.list=data
        $scope.loading=false

  $scope.add = ()->
    $scope.entity = {}
    $scope.editDialog = true

  $scope.edit = (item)->
    $scope.entity = angular.copy(item)
    $scope.editDialog = true

  $scope.save = ->
    $scope.entity.Channel=$scope.channel
    if $scope.entity.GroupId
      Group.edit {id:"(guid'#{$scope.entity.GroupId}')"},$scope.entity
      ,(data)->
        message.success "Edit group successfully."
        $scope.close()
        load()
    else
      debugger
      $scope.entity.GroupId=UUID.generate()
      Group.save $scope.entity
      ,(data)->
        message.success "Add group successfully."
        $scope.close()
        load()

  $scope.remove = (item)->
    message.confirm ->
      item.IsDeleted=true
      Group.edit {id:"(guid'#{item.GroupId}')"},item
      ,(data)->
          message.success "Delete group successfully."
          load()

  $scope.close = ->
    $scope.editDialog = false

  load()
]