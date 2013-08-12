CategoryController=["$scope","$routeParams","Category","Group", ($scope,$routeParams,Category,Group) ->
  $scope.loading=true
  Category.query 
    $filter:"IsDeleted eq false and Group/Url eq '#{$routeParams.group}'"
    ,(data)->
      $scope.list=data
      $scope.loading=false
  Group.query
    $filter:"Url eq '#{$routeParams.group}'"
    $expand:'Channel'
    ,(data)->
      $scope.group=data.value[0]
]