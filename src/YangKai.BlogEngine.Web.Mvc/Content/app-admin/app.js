
angular.module("app-admin", ['ngRoute', 'ngSanitize', 'ngAnimate', 'formatFilters', 'admin-dashboard', 'admin-basedata', 'admin-article', 'admin-board', 'admin-gallery', 'admin-system', 'admin.main.controllers', 'AccountServices', 'VersionServices', 'customDirectives', 'pasvaz.bindonce', 'ngProgress', 'ngStorage', 'FileUpload', 'ui.utils', 'ui.bootstrap']).config([
  "$locationProvider", function($locationProvider) {
    return $locationProvider.html5Mode(true);
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
]);

angular.module("app-login", ['admin.main.controllers', 'AccountServices']);
