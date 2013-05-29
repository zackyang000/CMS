angular.module("app", ['formatFilters','MessageServices'])

.config ["$locationProvider","$routeProvider", ($locationProvider,$routeProvider) ->
  $locationProvider.html5Mode(true)
  $routeProvider
  .when("/board",
    templateUrl: "/partials/Board/message-list.html"
    controller: BoardController)
  .when("/",
    templateUrl: "/partials/Home/index.html"
    controller: HomeController)
  .otherwise redirectTo: "/"
]
