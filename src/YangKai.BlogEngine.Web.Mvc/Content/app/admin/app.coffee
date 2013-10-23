angular.module("app-admin",
['ngRoute','ngSanitize','ngAnimate',
'formatFilters',
'admin-dashboard',
'admin-basedata',
'admin-article',
'admin-board',
'admin-gallery',
'MessageServices',
'ArticleServices',
'CommentServices',
'UserServices',
'ChannelServices',
'GroupServices',
'CategoryServices',
'GalleryServices',
'PhotoServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'FileUpload',
'ui.utils',
'ui.bootstrap'])
.config ["$locationProvider","$routeProvider","$httpProvider", ($locationProvider,$routeProvider,$httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
  $locationProvider.html5Mode(false).hashPrefix('!')
  $routeProvider.otherwise redirectTo: "/"
]