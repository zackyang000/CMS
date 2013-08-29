ArchivesController=["$scope","Channel", ($scope,Channel) ->
  $scope.$parent.title='Archives'
  $scope.$parent.showBanner=false

  $scope.load = ->
    $scope.loading=true
    Channel.archives (data)->
      $scope.list = data
      $scope.loading=false

  $scope.load()
]