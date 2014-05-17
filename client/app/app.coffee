angular.module("app",
['ngRoute'
'ngCookies'
'ngSanitize'
'index'
'article'
'board'
'about'
'gallery'
'framework.controllers'
'zy.services'
'zy.directives'
'zy.filters'
'zy.untils'
'pasvaz.bindonce'
'ngProgress'
'ngGrid'
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

.config(["$routeProvider",($routeProvider) ->
  $routeProvider
  .when "/admin",
    template:" "
    controller: -> window.location.href = "/admin"
  .otherwise redirectTo: "/"
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
        context.account = data
        context.auth.admin = true

      $rootScope.account=context.account
])