angular.module("app-admin",
['ngRoute'
'ngCookies'
'admin-dashboard'
'admin-basedata'
'admin-article'
'admin-board'
'admin-gallery'
'admin-system'
'framework.controllers'
'zy.services'
'zy.directives'
'zy.filters'
'pasvaz.bindonce'
'ngProgress'
'ngStorage'
'angularFileUpload'
'ui.utils'
'ui.bootstrap'
])

.config(["$locationProvider",($locationProvider) ->
  $locationProvider.html5Mode(true)
])

#ajax error handle.
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

#when route missing then goto 404 page.
.config(["$routeProvider",($routeProvider) ->
  $routeProvider.otherwise redirectTo: "/404"
])

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
      name:data.UserName
      email:data.Email
      avatar:data.Avatar
    context.auth.admin = true
    $rootScope.$broadcast "loginSuccessed"
  , ->
    $rootScope.$broadcast "logoutSuccessed"
])