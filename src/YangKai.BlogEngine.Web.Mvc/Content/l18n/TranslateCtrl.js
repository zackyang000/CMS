var TranslateCtrl;

TranslateCtrl = [
  "$scope", "$translate", function($scope, $translate) {
    $scope.uses = $translate.uses();
    return $scope.changeLanguage = function(langKey) {
      $scope.uses = langKey;
      return $translate.uses(langKey);
    };
  }
];
