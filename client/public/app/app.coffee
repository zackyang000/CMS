angular.module("app",
['ngRoute'
'ngCookies'
'ngSanitize'
'public-templates'
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
'ngProgress'
'ui.utils'
'pascalprecht.translate'
'ngStorage'
'angulartics'
'angulartics.google.analytics'
'angularUtils.directives.dirPagination'
])

.config ["$locationProvider", ($locationProvider) ->
  $locationProvider.html5Mode
    enabled: true
    requireBase: false
]

.config ["$routeProvider",($routeProvider) ->
  $routeProvider
  .when "/admin",
    template:" "
    controller: -> window.location.href = "/admin"
  .otherwise redirectTo: "/"
]

.config ["$translateProvider", ($translateProvider) ->
    $translateProvider.preferredLanguage('en')
    $translateProvider.useLocalStorage()
    $translateProvider
    .translations('en',translationsEN)
    .translations('zh',translationsZH)
]

.run ["$location", "$rootScope", ($location, $rootScope) ->
  $rootScope.$on "$routeChangeSuccess", (event, current) ->
    $rootScope.title = current.$$route?.title ? ''
]

.run ['ngProgress', (ngProgress) ->
  ngProgress.color('#5cb85c')
]

#get account info.
.run ["$rootScope","security","context", ($rootScope, security, context) ->
    security.autoLogin().then (data) ->
      if data
        context.account = data
        context.auth.admin = true
      $rootScope.account=context.account
]

.run ["$rootScope", ($rootScope) ->
    $rootScope.config = config
]
