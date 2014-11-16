angular.module("article-sider-search", [])

.controller('article-sider-search',
['$scope', '$location', ($scope, $location) ->
  $scope.search = ->
    $location.path("/search/#{$scope.key}")
])
