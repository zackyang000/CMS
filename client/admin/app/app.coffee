angular.module("app",
['ngRoute'
'ngCookies'
'ngSanitize'
'admin-templates'
'dashboard'
'category'
'article'
'gallery'
'system'
'dialogs'
'framework.controllers'
'zy.services'
'zy.directives'
'zy.filters'
'zy.untils'
'ngProgress'
'ngStorage'
'ngDialog'
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

.run ["$rootScope", ($rootScope) ->
  $rootScope.config = config
]
