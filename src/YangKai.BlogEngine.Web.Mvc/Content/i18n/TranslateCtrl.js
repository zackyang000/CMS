var TranslateCtrl;

TranslateCtrl = [
  "$scope", "$translate", function($scope, $translate) {
    return $scope.changeLanguage = function(langKey) {
      return $translate.uses(langKey);
    };
  }
];
