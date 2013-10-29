angular.module("app",
['ngRoute','ngSanitize','ngAnimate','ngCookies',
'l18n'
'formatFilters',
'index',
'article',
'board',
'about',
'issue',
'gallery',
'UserServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'ui.utils',
'ui.bootstrap',
'pascalprecht.translate',
'angulartics',
'angulartics.google.analytics'])
.config(["$locationProvider",($locationProvider) ->
  $locationProvider.html5Mode(false).hashPrefix('!')
])
.config(["$httpProvider",($httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
])
.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/"
])
.config(["$translateProvider",($translateProvider) ->
  $translateProvider.preferredLanguage('en')
  $translateProvider.useLocalStorage()
])
