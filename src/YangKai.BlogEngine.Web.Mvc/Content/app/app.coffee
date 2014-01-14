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
'main.controllers',
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
.run(["$location", "$rootScope", ($location, $rootScope) ->
  $rootScope.$on "$routeChangeSuccess", (event, current, previous) ->
    $rootScope.title = current.$$route.title ? ''
    $rootScope.showBanner = current.$$route.showBanner
])