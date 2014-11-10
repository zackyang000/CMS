angular.module("framework.controllers.top", [])

.controller('TopCtrl',
["$scope", "$http", "$location", '$window', "$translate", "context"
($scope, $http, $location, $window, $translate, context) ->
  $scope.use = $translate.use()

  $scope.changeLanguage = (langKey) ->
    $scope.use = langKey
    $translate.use(langKey)
    context.language = langKey

  $scope.login = ->
    $window.location.href='/admin/'
])
