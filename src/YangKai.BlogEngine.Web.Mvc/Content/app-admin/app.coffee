angular.module("app-admin",
['ngRoute','ngSanitize','ngAnimate',
'formatFilters',
'admin-dashboard',
'admin-basedata',
'admin-article',
'admin-board',
'admin-gallery',
'admin-system',
'admin.main.controllers',
'AccountServices',
'VersionServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'ngStorage',
'FileUpload',
'ui.utils',
'ui.bootstrap'])
.config(["$locationProvider",($locationProvider) ->
  $locationProvider.html5Mode(true)
])
.config(["$httpProvider",($httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
])
.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/"
])

angular.module("app-login", ['admin.main.controllers','AccountServices'])