HomeController=["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='首页'
  $scope.$parent.showBanner=true

  $http.get("/data/words.js").success (data) ->
    $scope.list = data
    debugger
    $scope.$parent.word = $scope.list[0]
  ]