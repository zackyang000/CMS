TranslateCtrl=["$scope","$translate",($scope,$translate) ->
  $scope.uses=$translate.uses()

  $scope.changeLanguage = (langKey) ->
    $scope.uses=langKey
    $translate.uses(langKey)
]
