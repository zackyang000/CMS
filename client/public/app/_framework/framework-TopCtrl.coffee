angular.module("framework.controllers.top", [])

.controller('TopCtrl',
["$scope", "$http", "$location", '$window', "$translate", "context"
($scope, $http, $location, $window, $translate, context) ->
  # 语言检测(首次进入的用户)
  unless $translate.use()
    defaultLanguage = config.languages[Object.keys(config.languages)[0]]
    $translate.use(defaultLanguage)

    userLanguage = navigator.language || navigator.browserLanguage
    if userLanguage
      for displayName, language of config.languages
        if language.toLowerCase() == userLanguage.toLowerCase()
          $translate.use(language)

  $scope.use = $translate.use()
  context.language = $scope.use

  $scope.changeLanguage = (langKey) ->
    $scope.use = langKey
    $translate.use(langKey)
    context.language = langKey

  $scope.login = ->
    $window.location.href='/admin/'
])
