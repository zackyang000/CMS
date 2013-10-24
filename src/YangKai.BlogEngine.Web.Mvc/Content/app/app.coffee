angular.module("app",
['ngRoute','ngSanitize','ngAnimate'
'formatFilters',
'index',
'article',
'board',
'about',
'issue',
'UserServices',
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
  $routeProvider.otherwise redirectTo: "/"
]
