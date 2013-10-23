angular.module("app",
['ngRoute','ngSanitize','ngAnimate'
'formatFilters',
'issue',
'MessageServices',
'ArticleServices',
'CommentServices',
'UserServices',
'ChannelServices',
'GalleryServices',
'PhotoServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'ui.utils',
'ui.bootstrap',
'angulartics', 
'angulartics.google.analytics'])
.config ["$locationProvider","$routeProvider","$httpProvider", ($locationProvider,$routeProvider,$httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
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
  .when("/archives",
    templateUrl: "/partials/Article/archives.html"
    controller: ArchivesController)
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

angular.module("app-login",['ngRoute','ngSanitize','ngAnimate','UserServices'])