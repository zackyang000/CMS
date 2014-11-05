#todo: 该模块存在bug, 如果ajax出错, 可能无法触发transformResponse导致loading无法关闭

angular.module("zy.untils.globalLoading", [])

.constant('global-loading-app', {})

.config(['$httpProvider', '$provide', 'global-loading-app', ($httpProvider, $provide, app) ->
    # global loading status
    status =
      count: 0
      loading: false
      cancel : ->
        status.count = 0
        status.loading = false
        app.loading(false)

    # ajax begin
    $httpProvider.defaults.transformRequest.push (data) ->
      status.count += 1
      if !status.loading
        window.setTimeout (->
          if !status.loading and status.count > 0
            status.loading = true
            app.loading(true)
        ), 0
      return data

    # ajax end
    $httpProvider.defaults.transformResponse.push (data) ->
      status.count -= 1
      status.cancel()  if status.loading and status.count is 0
      return data


    $provide.factory 'ajaxErrorHandler', ["$rootScope", "$q", "messager", ($rootScope, $q, messager) ->
      success = (response) ->
        response
      error = (response) ->
        messager.error response.data
        status.cancel()
        $q.reject(response)
      (promise) ->
        promise.then success, error
    ]

    $httpProvider.interceptors.push('ajaxErrorHandler')
  ])

#register global ajax loading
.run(["$rootScope", "global-loading-app", ($rootScope, app) ->
    app.loading = (val) ->
      $rootScope.loading = val
])

