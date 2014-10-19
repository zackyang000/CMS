angular.module("app",
['ngRoute'
'ngCookies'
'ngSanitize'
'dashboard'
'category'
'article'
'gallery'
'system'
'framework.controllers'
'zy.services'
'zy.directives'
'zy.filters'
'zy.untils'
'pasvaz.bindonce'
'ngProgress'
'ngStorage'
'angularFileUpload'
'ui.utils'
'ui.bootstrap'
])

.config ["$locationProvider", ($locationProvider) ->
  $locationProvider.html5Mode
    enabled: true
    requireBase: false
]

#when route missing then goto 404 page.
.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/404"
])

.run ['ngProgress', (ngProgress) ->
  ngProgress.color('#5cb85c')
]

#hide current neg-progress when route start to change.
.run(["$rootScope","progress", ($rootScope,progress) ->
  $rootScope.$on '$routeChangeStart', ->
    progress.complete()
])

#try to auto login.
.run(["$rootScope","security","$location","context",
($rootScope,security,$location, context) ->
  current = $location.path()
  if current != '/login'
    $rootScope.__returnUrl = current
  $location.path('/login').replace()
  security.autoLogin().then (data) ->
    context.account=
      name:data.name
      email:data.email
    context.auth.admin = true
    $rootScope.$broadcast "loginSuccessed"
  , ->
    $rootScope.$broadcast "logoutSuccessed"
])