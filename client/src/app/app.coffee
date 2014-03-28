angular.module("app",
['ngRoute'
'ngSanitize'
'ngAnimate'
'ngCookies'
'index'
'article'
'board'
'about'
'issue'
'gallery'
'main.controllers'
'zy.services'
'zy.directives'
'zy.filters'
'pasvaz.bindonce'
'ngProgress'
'ui.utils'
'ui.bootstrap'
'pascalprecht.translate'
'ngStorage'
'angulartics'
'angulartics.google.analytics'
])

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
    $translateProvider.preferredLanguage('en')
    $translateProvider.useLocalStorage()
    $translateProvider
    .translations('en',translationsEN)
    .translations('zh',translationsZH)
  ])

.run(["$location", "$rootScope", ($location, $rootScope) ->
  $rootScope.$on "$routeChangeSuccess", (event, current, previous) ->
    $rootScope.title = current.$$route?.title ? ''
])

#get account info.
.run(["$rootScope","security","context", "$localStorage"
  ($rootScope,security, context, $localStorage) ->
    security.autoLogin().then (data) ->
      if data
        context.account =
          name:data.UserName
          email:data.Email
          avatar:data.Avatar
        context.auth.admin = true

      $rootScope.account=context.account
])