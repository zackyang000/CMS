GlobalController=
["$scope","$http","$location",'$window',"Channel" ,"account"
($scope,$http,$location,$window,Channel,account) ->
  
  Channel.query 
    $orderby:'OrderId' 
    $filter:'IsDeleted eq false'
    $expand:'Groups'
    $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
  ,(data)->
    $scope.Channels=data.value

  $scope.search = ->
    $window.location.href="/search/#{$scope.key}"
]
