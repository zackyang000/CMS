angular.module("app-admin",
['ngRoute','ngSanitize','ngAnimate',
'formatFilters',
'admin-dashboard',
'admin-basedata',
'admin-article',
'admin-board',
'admin-gallery',
'admin-system',
'ctrl.main',
'AccountServices',
'VersionServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'FileUpload',
'ui.utils',
'ui.bootstrap'])
.config ["$locationProvider","$routeProvider","$httpProvider", ($locationProvider,$routeProvider,$httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
  $locationProvider.html5Mode(true)
  $routeProvider.otherwise redirectTo: "/"
]

angular.module("app-login",
['ngRoute','ngSanitize','ngAnimate','UserServices'])