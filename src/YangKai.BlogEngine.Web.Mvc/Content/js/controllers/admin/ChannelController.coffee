ChannelController=["$scope","$dialog","Channel", ($scope,$dialog,Channel) ->
  $scope.loading=true
  Channel.query (data)->
    $scope.list = data

  $scope.opts=
    backdrop: true
    keyboard: true
    backdropClick: true
    dialogFade:true
    backdropFade:true


  $scope.open = ->
    $scope.shouldBeOpen = true

  $scope.close = ->
    $scope.closeMsg = 'I was closed at: ' + new Date()
    $scope.shouldBeOpen = false
]