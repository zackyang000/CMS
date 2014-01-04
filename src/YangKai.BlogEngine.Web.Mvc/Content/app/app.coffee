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
'ctrl.main',
'AccountServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'ui.utils',
'ui.bootstrap',
'pascalprecht.translate',
'angulartics',
'angulartics.google.analytics'])
.config(["$locationProvider",($locationProvider) ->
  $locationProvider.html5Mode(true)
])
.config(["$httpProvider",($httpProvider) ->
  $httpProvider.responseInterceptors.push(interceptor)  
])
.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/"
])
.config(["$translateProvider",($translateProvider) ->
  $translateProvider.preferredLanguage('zh')
  $translateProvider.useLocalStorage()
])
