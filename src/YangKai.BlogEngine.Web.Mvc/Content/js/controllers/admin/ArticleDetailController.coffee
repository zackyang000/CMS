ArticleDetailController=["$scope","$routeParams","Article","Channel",
($scope,$routeParams,Article,Channel) ->
  $scope.id =$routeParams.id

  $scope.channels=Channel.query $expand:'Groups,Groups/Categorys'

  $scope.getGroups = ->
    return undefined if $scope.channels.value is undefined
    for item in $scope.channels.value
      return item.Groups if item.ChannelId==$scope.channelId

  $scope.getCategories = ->
    return undefined if $scope.getGroups() is undefined
    for item in $scope.getGroups()
      debugger
      return item.Categorys if item.GroupId==$scope.groupId
]