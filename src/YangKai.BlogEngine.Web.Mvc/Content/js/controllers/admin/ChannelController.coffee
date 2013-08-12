ChannelController=["$scope","Channel", ($scope,Channel) ->
  $scope.loading=true
  Channel.query (data)->
    $scope.list = data
    $scope.loading=false
]