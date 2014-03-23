angular.module("app-admin",
['ngRoute','ngSanitize','ngAnimate',
'formatFilters',
'admin-dashboard',
'admin-basedata',
'admin-article',
'admin-board',
'admin-gallery',
'admin-system',
'framework.controllers',
'zy.services.security',
'zy.services.progress',
'zy.services.messager',
'AccountServices',
'VersionServices',
'customDirectives',
'pasvaz.bindonce',
'ngProgress',
'ngStorage',
'FileUpload',
'ui.utils',
'ui.bootstrap'])

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
  $routeProvider.otherwise redirectTo: "/404"
])

#hide current neg-progress when route start to change
.run(["$rootScope","progress", ($rootScope,progress) ->
    $rootScope.$on '$routeChangeStart', ->
      progress.complete()
  ])

#auto login
.run(["$rootScope","security","$location","$http", "$q",
($rootScope,security,$location) ->
  current = $location.path()
  if current != '/login'
    $rootScope.__returnUrl = current
  $location.path('/login').replace()
  security.autoLogin().then (data)->
    $rootScope.$broadcast "loginSuccessed"
  , ->
    $rootScope.$broadcast "logoutSuccessed"
])