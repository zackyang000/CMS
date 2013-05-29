angular.module("app", ['formatFilters','MessageServices'])
.config ["$routeProvider", ($routeProvider) ->
  $routeProvider
  .when("/board",
    templateUrl: "/partials/Board/message-list.html"
    controller: BoardController)
  .when("/",
    templateUrl: "/partials/Board/message-list.html"
    controller: BoardController)
  .otherwise redirectTo: "/"
]
