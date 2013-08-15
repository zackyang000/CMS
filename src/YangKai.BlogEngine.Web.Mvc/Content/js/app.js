var interceptor;

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

angular.module("app-login", ['UserServices', 'ui.utils', 'ui.bootstrap']);

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

interceptor = [
  "$rootScope", "$q", function(scope, $q) {
    var error, success;
    success = function(response) {
      return response;
    };
    error = function(response) {
      var status;
      status = response.status;
      if (status === 401) {
        window.location = "/admin/login";
      } else if (status === 400) {
        alert(response.data['odata.error'].innererror.message);
      } else if (status === 500) {
        alert(response.data['odata.error'].innererror.message);
      }
      return $q.reject(response);
    };
    return function(promise) {
      return promise.then(success, error);
    };
  }
];
