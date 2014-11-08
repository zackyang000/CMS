angular.module("framework.controllers.top", [])

.controller('TopCtrl',
["$scope", "$http", "$location", '$window', "$translate", "context"
($scope, $http, $location, $window, $translate, context) ->
  $scope.languages = config.languages
  $scope.use = $translate.use()
  context.language = $scope.use

  $scope.changeLanguage = (langKey) ->
    $scope.use = langKey
    $translate.use(langKey)
    context.language = langKey

  $scope.login = ->
    $window.location.href='/admin/'
])
