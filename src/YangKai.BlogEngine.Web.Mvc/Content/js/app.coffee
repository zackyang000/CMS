angular.module("app",
['formatFilters',
'MessageServices',
'ArticleServices',
'CommentServices',
'customDirectives',#自定义指令
'ui.utils',
'ui.bootstrap',
'$strap.directives'])

.config ["$locationProvider","$routeProvider", ($locationProvider,$routeProvider) ->
  $locationProvider.html5Mode(false).hashPrefix('!')
  $routeProvider
  .when("/list/:channel/:group/:type/:query",
    templateUrl: "/partials/Article/list.html"
    controller: ArticleListController)
  .when("/list/:channel/:group",
    templateUrl: "/partials/Article/list.html"
    controller: ArticleListController)
  .when("/list/:channel",
    templateUrl: "/partials/Article/list.html"
    controller: ArticleListController)
  .when("/search/:key",
    templateUrl: "/partials/Article/list.html"
    controller: ArticleListController)
  .when("/post/:url",
    templateUrl: "/partials/Article/detail.html"
    controller: ArticleDetailController)
  .when("/board",
    templateUrl: "/partials/message.html"
    controller: MessageController)
  .when("/about",
    templateUrl: "/partials/about.html"
    controller: AboutController)
  .when("/",
    templateUrl: "/partials/index.html"
    controller: HomeController)
  .otherwise redirectTo: "/"
]