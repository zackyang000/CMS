angular.module("framework.controllers.head",['resource.categories'])

.controller('HeaderCtrl',
["$scope", "$http", "$location", "$window", "Categories"
($scope, $http, $location, $window, Categories) ->

  $scope.$on "categoryChange", (event, categoryUrl) ->
    $scope.currentCategoryUrl = categoryUrl || $scope.defaultCategoryUrl

  Categories.query
    $orderby: 'order'
  ,(data) ->
    $scope.categories = data.value
    $scope.defaultCategoryUrl = category.url for category in $scope.categories when category.main

  $scope.isActiveCategory = (category) ->
    #home page
    return true if category.url == $scope.defaultCategoryUrl && $location.path() == "/"
    #article list
    return true if $location.path().indexOf(category.url) > -1
    #article detail
    return $location.path().indexOf("/post") > -1 && category.url.indexOf($scope.currentCategoryUrl) > -1

  $scope.isActive = (route) ->
    $location.path().indexOf(route) == 0

  $scope.search = ->
    $scope.displaySearchDialog = false
    $location.path("/search/#{$scope.key}")
])
