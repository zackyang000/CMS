angular.module("app",
['ngRoute','ngSanitize','ngAnimate','ngCookies',
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

.config(["$httpProvider", ($httpProvider) ->
    $httpProvider.responseInterceptors.push ["$rootScope", "$q", ($rootScope, $q) ->
      success = (response) ->
        response
      error = (response) ->
        debugger
        $q.reject(response)
      (promise) ->
        promise.then success, error
    ]
])

.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/"
])
.config(["$translateProvider",($translateProvider) ->
    $translateProvider.preferredLanguage('en-us')
    $translateProvider.useLocalStorage()
    $translateProvider
    .translations('en',translationsEN)
    .translations('zh',translationsZH)
  ])

.run(["$location", "$rootScope", ($location, $rootScope) ->
  $rootScope.$on "$routeChangeSuccess", (event, current, previous) ->
    $rootScope.title = current.$$route?.title ? ''
    #todo
    #todo
    #todo
    #todo
    #todo
    #todo
    
])