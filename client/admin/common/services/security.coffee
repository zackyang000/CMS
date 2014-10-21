angular.module("zy.services.security", ['resource.users'])
.factory "security", ['Users','$q', "$http", (Users, $q, $http) ->
  autoLogin: ->
    deferred = $q.defer()
    token = $.cookie('authorization')
    if token
      $http.defaults.headers.common['authorization'] = token
      $http.post "#{config.apiHost}/auto-login", undefined
      .success (data) ->
        deferred.resolve data
      .error (error) ->
        #$.removeCookie('authorization', { path: '/' })
        deferred.reject undefined
    else
      deferred.reject undefined
    deferred.promise

  login: (user) ->
    deferred = $q.defer()
    $http.post "#{config.apiHost}/login", { name: user.name, password: user.password }
    .success (data, status, headers) ->
      token = headers('authorization')
      $http.defaults.headers.common['authorization'] = token
      if user.remember
        $.cookie('authorization', token, {expires: 180, path: '/'})
      else
        $.cookie('authorization', token, { path: '/'})
      deferred.resolve data
    .error (error) ->
      deferred.reject undefined
    deferred.promise

  logoff: ->
    deferred = $q.defer()
    $http.post "#{config.apiHost}/logoff", undefined
    .success (data) ->
      $.removeCookie('authorization', { path: '/' })
      delete $http.defaults.headers.common['authorization']
      deferred.resolve "OK"
    deferred.promise
]