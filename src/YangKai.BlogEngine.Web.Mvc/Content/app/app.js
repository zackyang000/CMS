

angular.module("app", ['ngRoute', 'ngSanitize', 'ngAnimate', 'ngCookies', 'l18n', 'formatFilters', 'index', 'article', 'board', 'about', 'issue', 'gallery', 'UserServices', 'customDirectives', 'pasvaz.bindonce', 'ngProgress', 'ui.utils', 'ui.bootstrap', 'pascalprecht.translate', 'angulartics', 'angulartics.google.analytics']).config([
  "$locationProvider", function($locationProvider) {
    return $locationProvider.html5Mode(false).hashPrefix('!');
  }
]).config([
  "$httpProvider", function($httpProvider) {
    return $httpProvider.responseInterceptors.push(interceptor);
  }
]).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.otherwise({
      redirectTo: "/"
    });
  }
]).config([
  "$translateProvider", function($translateProvider) {
    $translateProvider.preferredLanguage('zh');
    return $translateProvider.useLocalStorage();
  }
]);
