
angular.module("app", ['formatFilters', 'MessageServices', 'ArticleServices', 'CommentServices', 'UserServices', 'customDirectives', 'ui.utils', 'ui.bootstrap']).config([
  "$locationProvider", "$routeProvider", function($locationProvider, $routeProvider) {
    $locationProvider.html5Mode(false).hashPrefix('!');
    return $routeProvider.when("/list/:channel/:group/:type/:query", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleListController
    }).when("/list/:channel/:group", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleListController
    }).when("/list/:channel", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleListController
    }).when("/search/:key", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleListController
    }).when("/post/:url", {
      templateUrl: "/partials/Article/detail.html",
      controller: ArticleDetailController
    }).when("/board", {
      templateUrl: "/partials/message.html",
      controller: MessageController
    }).when("/about", {
      templateUrl: "/partials/about.html",
      controller: AboutController
    }).when("/", {
      templateUrl: "/partials/index.html",
      controller: HomeController
    }).otherwise({
      redirectTo: "/"
    });
  }
]);

angular.module("app-login", ['UserServices']);

angular.module("app-admin", ['formatFilters', 'MessageServices', 'ArticleServices', 'CommentServices', 'UserServices', 'ChannelServices', 'GroupServices', 'CategoryServices', 'customDirectives', 'ui.utils', 'ui.bootstrap']).config([
  "$locationProvider", "$routeProvider", function($locationProvider, $routeProvider) {
    $locationProvider.html5Mode(false).hashPrefix('!');
    return $routeProvider.when("/channel", {
      templateUrl: "/partials/Admin/channel.html",
      controller: ChannelController
    }).when("/channel(':channel')/group", {
      templateUrl: "/partials/Admin/group.html",
      controller: GroupController
    }).when("/channel(':channel')/group", {
      templateUrl: "/partials/Admin/group.html",
      controller: GroupController
    }).when("/channel(':channel')/group(':group')/category", {
      templateUrl: "/partials/Admin/category.html",
      controller: CategoryController
    }).otherwise({
      redirectTo: "/channel"
    });
  }
]);
