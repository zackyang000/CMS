angular.module("app",
['formatFilters',
'MessageServices',
'ArticleServices',
'CommentServices',
'UserServices',
'ChannelServices',
'customDirectives',
'ngProgress',
'ui.utils',
'ui.bootstrap'])
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

angular.module("app-login",['UserServices',
'ui.utils',
'ui.bootstrap'])
.config ["$locationProvider","$routeProvider","$httpProvider", 
($locationProvider,$routeProvider,$httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)
]

angular.module("app-admin",['formatFilters',
'MessageServices',
'ArticleServices',
'CommentServices',
'UserServices',
'ChannelServices',
'GroupServices',
'CategoryServices',
'customDirectives',
'ngProgress',
'FileUpload',
'ui.utils',
'ui.bootstrap'])
.config ["$locationProvider","$routeProvider","$httpProvider", ($locationProvider,$routeProvider,$httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
  $locationProvider.html5Mode(false).hashPrefix('!')
  $routeProvider
  #Channel
  .when("/channel",
    templateUrl: "/partials/Admin/channel/list.html"
    controller: AdminChannelController)
  #Group
  .when("/channel(':channel')/group",
    templateUrl: "/partials/Admin/group/list.html"
    controller: AdminGroupController)
  #Category
  .when("/channel(':channel')/group(':group')/category",
    templateUrl: "/partials/Admin/category/list.html"
    controller: AdminCategoryController)
  #Article
  .when("/article",
    templateUrl: "/partials/Admin/article/list.html"
    controller: AdminArticleController)
  .when("/article(':id')",
    templateUrl: "/partials/Admin/article/detail.html"
    controller: AdminArticleDetailController)
  .when("/article/new",
    templateUrl: "/partials/Admin/article/detail.html"
    controller: AdminArticleDetailController)
  #Message boards
  .when("/board",
    templateUrl: "/partials/Admin/board/list.html"
    controller: AdminBoardController)
  #home
  #.when("/",
  #  templateUrl: "/partials/Admin/index.html"
  #  controller: AdminHomeController)
  .otherwise redirectTo: "/article"
]

interceptor = ["$rootScope", "$q", (scope, $q) ->
  success = (response) ->
    response
  error = (response) ->
    debugger
    status = response.status
    if status is 401
      message.error '401 Unauthorized'
    else if status is 400
      message.error response.data['odata.error'].innererror.message
    else if status is 500
      message.error response.data['odata.error'].innererror.message
    $q.reject(response)
  (promise) ->
    promise.then success, error
]