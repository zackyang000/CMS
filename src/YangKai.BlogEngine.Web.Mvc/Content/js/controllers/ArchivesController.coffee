ArchivesController=["$scope","Channel", ($scope,Channel) ->
  $scope.$parent.title='Archives'
  $scope.$parent.showBanner=false

  $scope.load = ->
    Channel.archives (data)->
      $scope.list = data

  $scope.load()
]