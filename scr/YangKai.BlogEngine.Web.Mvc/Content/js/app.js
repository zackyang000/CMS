
angular.module("app", ['formatFilters', 'MessageServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/partials/Board/message-list.html",
      controller: BoardController
    }).when("/", {
      templateUrl: "/partials/Board/message-list.html",
      controller: BoardController
    }).otherwise({
      redirectTo: "/"
    });
  }
]);
