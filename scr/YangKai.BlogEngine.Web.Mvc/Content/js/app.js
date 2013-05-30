
angular.module("app", ['formatFilters', 'MessageServices']).config([
  "$locationProvider", "$routeProvider", function($locationProvider, $routeProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');
    return $routeProvider.when("/board", {
      templateUrl: "/partials/Board/message-list.html",
      controller: BoardController
    }).when("/", {
      templateUrl: "/partials/Home/index.html",
      controller: HomeController
    }).otherwise({
      redirectTo: "/"
    });
  }
]);
