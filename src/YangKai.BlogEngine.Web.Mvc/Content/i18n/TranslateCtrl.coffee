TranslateCtrl=["$scope","$translate",($scope,$translate) ->
  $scope.changeLanguage = (langKey) ->
    $translate.uses(langKey)
]
