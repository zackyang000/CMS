GlobalController=["$scope","$http","$location",'$window', ($scope,$http,$location,$window) ->
  $http.get("/admin/getuser").success (data) ->
    if data.Email
      data.Gravatar='http://www.gravatar.com/avatar/' + md5(data.Email) 
    else
      data.Gravatar='/Content/img/avatar.png'
    $scope.User=data

  $scope.search = ->
    debugger
    $window.location.href="/#!/search/#{$scope.key}"
]
