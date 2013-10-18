HomeController=["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='Home'
  $scope.$parent.showBanner=true

  $scope.loading="Loading"
  $http.get("/Content/data/words.js").success (data) ->
    $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1)-1]
    $scope.loading=""
  ]