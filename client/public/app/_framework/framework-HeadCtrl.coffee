angular.module("framework.controllers.head", [])

.controller('HeaderCtrl',
["$scope", "$http", "$location", "$window", "dataCacheCategories"
($scope, $http, $location, $window, dataCacheCategories) ->

  $scope.$on "categoryChange", (event, categoryUrl) ->
    $scope.currentCategoryUrl = categoryUrl || $scope.defaultCategoryUrl

  $scope.categories = dataCacheCategories
  $scope.defaultCategoryUrl = (category.url for category in $scope.categories when category.main)[0]

  $scope.isActiveCategory = (category) ->
    #home page
    return true if category.url == $scope.defaultCategoryUrl && $location.path() == "/"
    #article list
    return true if $location.path().indexOf(category.url) > -1
    #article detail
    return $location.path().indexOf("/post") > -1 && category.url.indexOf($scope.currentCategoryUrl) > -1

  $scope.isActive = (route) ->
    $location.path().indexOf(route) == 0
])
