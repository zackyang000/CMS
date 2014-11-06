angular.module("zy.untils.ajaxLoading", [])

.factory 'globalLoading', ["$rootScope", "$q", ($rootScope, $q) ->
  activeRequests = 0
  started = ->
    activeRequests++
    $rootScope.loading = true

  ended = ->
    activeRequests--
    $rootScope.loading = false  if activeRequests is 0

  request: (config) ->
    started()
    config or $q.when(config)

  response: (response) ->
    ended()
    response or $q.when(response)

  responseError: (rejection) ->
    ended()
    $q.reject rejection
]

.config(['$httpProvider', ($httpProvider) ->
    $httpProvider.interceptors.push('globalLoading')
])