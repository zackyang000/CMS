
angular.module("l18n", ['pascalprecht.translate']).config([
  "$translateProvider", function($translateProvider) {
    return $translateProvider.translations('en', translationsEN).translations('zh', translationsZH);
  }
]);
