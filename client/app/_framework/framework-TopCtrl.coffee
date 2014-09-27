angular.module("framework.controllers.top", [])

.controller('TopCtrl',
["$scope", "$http", "$location", '$window', "$translate"
($scope, $http, $location, $window, $translate) ->

  $scope.uses = $translate.use()
  $scope.changeLanguage = (langKey) ->
    $scope.use = langKey
    $translate.use(langKey)

  $scope.login = ->
    $window.location.href='/admin/'
])
