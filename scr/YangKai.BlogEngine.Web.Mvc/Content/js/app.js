
angular.module("app", ['formatFilters', 'MessageServices']).config([
  "$locationProvider", "$routeProvider", function($locationProvider, $routeProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');
    return $routeProvider.when("/list/:channel/:group/:page/:type/:query", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleController
    }).when("/list/:channel/:group/:type/:query", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleController
    }).when("/list/:channel/:group/:page", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleController
    }).when("/list/:channel/:group", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleController
    }).when("/list/:channel", {
      templateUrl: "/partials/Article/list.html",
      controller: ArticleController
    }).when("/board", {
      templateUrl: "/partials/Board/message-list.html",
      controller: BoardController
    }).when("/about", {
      templateUrl: "/partials/About/index.html",
      controller: AboutController
    }).when("/", {
      templateUrl: "/partials/Home/index.html",
      controller: HomeController
    }).otherwise({
      redirectTo: "/"
    });
  }
]);
