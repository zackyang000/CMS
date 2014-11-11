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
  $routeProvider.otherwise redirectTo: "/"
]

.config ["$translateProvider", ($translateProvider) ->
    $translateProvider.useLocalStorage()
    $translateProvider
      .translations('en-us',translationsEN)
      .translations('zh-cn',translationsZH)
]

.run ["$location", "$rootScope", ($location, $rootScope) ->
  $rootScope.$on "$routeChangeSuccess", (event, current) ->
    $rootScope.title = current.$$route?.title ? ''
]

.run ['ngProgress', (ngProgress) ->
  ngProgress.color('#5cb85c')
]

#get account info.
.run ["$rootScope", "security", "context", ($rootScope, security, context) ->
    security.autoLogin().then (data) ->
      if data
        context.account = data
        context.auth.admin = true
      $rootScope.account = context.account
]

# get user language
.run ['$translate', 'context', ($translate, context) ->
  # first time visit
  currentLanguage = $translate.use()
  supportLanguages = (language for displayName, language of config.languages)
  unless currentLanguage in supportLanguages
    $translate.use(supportLanguages[0])
    browserLanguage = navigator.language || navigator.browserLanguage
    if browserLanguage
      for language in supportLanguages
        if language.toLowerCase() == browserLanguage.toLowerCase()
          $translate.use(language)
          break

  context.language = $translate.use()
]

.run ["$rootScope", ($rootScope) ->
  $rootScope.config = config
]
