angular.module("app", ['boardServices']).config ["$routeProvider", ($routeProvider) ->
  $routeProvider
  .when("/board",
    templateUrl: "/partials/Board/message-list.html"
    controller: BoardController)
  .when("/",
    templateUrl: "/partials/Board/message-list.html"
    controller: BoardController)
  .otherwise redirectTo: "/"
]

angular.module("boardServices", ["ngResource"]).factory "Message", ($resource) ->
  $resource "/board", {},
    query:
      method: "GET"
      isArray: true
