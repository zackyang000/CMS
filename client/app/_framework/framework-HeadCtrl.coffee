angular.module("framework.controllers.head",['resource.categories'])

.controller('HeaderCtrl',
  ["$scope","$http","$location",'$window',"Categories" ,"$timeout"
    ($scope,$http,$location,$window,Categories,$timeout) ->
      Categories.query (data) ->
        $scope.categories = data.value

      $scope.isActiveChannel = (category) ->
        #home page
        return true if category.main && ($location.path() == "/" || $location.path() == "/list")
        #article list
        return true if $location.path().indexOf(category.Url) > -1
        #article detail
        return $location.path().indexOf("/post") > -1 && category.url.indexOf($scope.category) > -1

      $scope.isActive = (route) ->
        route == $location.path()

      $scope.search = ->
        $location.path("/search/#{$scope.key}")
  ])
