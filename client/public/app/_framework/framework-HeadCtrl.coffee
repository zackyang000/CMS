angular.module("framework.controllers.head",['resource.categories'])

.controller('HeaderCtrl',
["$scope", "$http", "$location", "$window", "Categories"
($scope, $http, $location, $window, Categories) ->

  $scope.$on "categoryChange", (event, categoryName) ->
    $scope.currentCategoryName = categoryName || $scope.defaultCategoryName

  Categories.query (data) ->
    $scope.categories = data.value
    $scope.defaultCategoryName = category.name for category in $scope.categories when category.main

  $scope.isActiveCategory = (category) ->
    #home page
    return true if category.main && ($location.path() == "/" || $location.path() == "/list")
    #article list
    return true if $location.path().indexOf(category.name) > -1
    #article detail
    return $location.path().indexOf("/post") > -1 && category.name.indexOf($scope.currentCategoryName) > -1

  $scope.isActive = (route) ->
    $location.path().indexOf(route) == 0

  $scope.search = ->
    $scope.displaySearchDialog = false
    $location.path("/search/#{$scope.key}")
])
