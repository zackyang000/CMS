
angular.module("app-admin", ['ngRoute', 'ngSanitize', 'ngAnimate', 'formatFilters', 'admin-dashboard', 'admin-basedata', 'admin-article', 'admin-board', 'admin-gallery', 'admin-system', 'MessageServices', 'ArticleServices', 'CommentServices', 'ChannelServices', 'GroupServices', 'GalleryServices', 'PhotoServices', 'UserServices', 'AccountServices', 'VersionServices', 'customDirectives', 'pasvaz.bindonce', 'ngProgress', 'FileUpload', 'ui.utils', 'ui.bootstrap']).config([
  "$locationProvider", "$routeProvider", "$httpProvider", function($locationProvider, $routeProvider, $httpProvider) {
    $httpProvider.responseInterceptors.push(interceptor);
    $locationProvider.html5Mode(true);
    return $routeProvider.otherwise({
      redirectTo: "/"
    });
  }
]);

angular.module("app-login", ['ngRoute', 'ngSanitize', 'ngAnimate', 'UserServices']);
