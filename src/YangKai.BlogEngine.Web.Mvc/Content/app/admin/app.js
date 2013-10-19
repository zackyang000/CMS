
angular.module("app-admin", ['formatFilters', 'admin-dashboard', 'admin-basedata', 'admin-article', 'admin-board', 'admin-gallery', 'MessageServices', 'ArticleServices', 'CommentServices', 'UserServices', 'ChannelServices', 'GroupServices', 'CategoryServices', 'GalleryServices', 'PhotoServices', 'customDirectives', 'pasvaz.bindonce', 'ngProgress', 'FileUpload', 'ui.utils', 'ui.bootstrap']).config([
  "$locationProvider", "$routeProvider", "$httpProvider", function($locationProvider, $routeProvider, $httpProvider) {
    $httpProvider.responseInterceptors.push(interceptor);
    $locationProvider.html5Mode(false).hashPrefix('!');
    return $routeProvider.otherwise({
      redirectTo: "/"
    });
  }
]);
