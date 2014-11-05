angular.module("zy.untils.ajaxErrorHandler", [])

.factory 'ajaxErrorHandler', ["$rootScope", "$q", "messager", ($rootScope, $q, messager) ->
  success = (response) ->
    response
  error = (response) ->
    messager.error response.data
    status.cancel()
    $q.reject(response)
  (promise) ->
    promise.then success, error
]

.config(['$httpProvider', '$provide', ($httpProvider, $provide) ->
    $httpProvider.interceptors.push('ajaxErrorHandler')
  ])
