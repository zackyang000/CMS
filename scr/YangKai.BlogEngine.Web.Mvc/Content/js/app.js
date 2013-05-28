
angular.module("app", ['formatFilters', 'boardServices']).config([
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

angular.module("boardServices", ["ngResource"]).factory("Message", [
  '$resource', function($resource) {
    return $resource("/board/list", {}, {
      query: {
        method: "GET",
        isArray: true
      }
    });
  }
]);
