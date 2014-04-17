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

.config(["$httpProvider", ($httpProvider ) ->
    $httpProvider.responseInterceptors.push ["$rootScope", "$q", "messager", ($rootScope, $q, messager) ->
      success = (response) ->
        response
      error = (response) ->
        if response.data['odata.error']? and response.data['odata.error'].innererror? and response.data['odata.error'].innererror.message?
          messager.error response.data['odata.error'].innererror.message
        $q.reject(response)
      (promise) ->
        promise.then success, error
    ]
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
        context.account =
          name:data.UserName
          email:data.Email
          avatar:data.Avatar
        context.auth.admin = true

      $rootScope.account=context.account
])