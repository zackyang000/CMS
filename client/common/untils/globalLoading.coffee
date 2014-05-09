angular.module("zy.untils.globalLoading", [])

.constant('global-loading-app', {})

.config(['$httpProvider', 'global-loading-app',($httpProvider, app) ->
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

    $httpProvider.responseInterceptors.push ["$rootScope", "$q", "messager", ($rootScope, $q, messager) ->
      success = (response) ->
        response
      error = (response) ->
        messager.error response
        status.cancel()
        $q.reject(response)
      (promise) ->
        promise.then success, error
    ]
  ])

#register global ajax loading
.run(["$rootScope","global-loading-app", ($rootScope, app) ->
    app.loading = (val) ->
      $rootScope.loading = val
])

