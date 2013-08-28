HomeController=["$scope","$http", ($scope,$http) ->
  $scope.$parent.title='Home'
  $scope.$parent.showBanner=true

  $http.get("/data/words.js").success (data) ->
    $scope.$parent.word = data[Math.floor(Math.random() * data.length + 1)-1]
  ]