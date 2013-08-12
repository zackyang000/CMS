GroupController=["$scope","$routeParams","Group","Channel", ($scope,$routeParams,Group,Channel) ->
  $scope.loading=true
  Group.query 
    $filter:"IsDeleted eq false and Channel/Url eq '#{$routeParams.channel}'"
    ,(data)->
      $scope.list=data
      $scope.loading=false
  Channel.query
    $filter:"Url eq '#{$routeParams.channel}'"
    ,(data)->
      $scope.channel=data.value[0]
]