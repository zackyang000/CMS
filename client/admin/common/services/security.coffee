angular.module("zy.services.security", ['resource.users', 'ipCookie'])
.factory "security", ['Users','$q', "$http", "ipCookie", (Users, $q, $http, ipCookie) ->
  autoLogin: ->
    deferred = $q.defer()
    token = ipCookie('authorization')
    if token
      $http.defaults.headers.common['authorization'] = token
      $http.post "#{config.url.api}/auto-login", undefined
      .success (data) ->
        deferred.resolve data
      .error (error) ->
        ipCookie.remove('authorization', { path: '/' })
        deferred.reject undefined
    else
      deferred.reject undefined
    deferred.promise

  login: (user) ->
    deferred = $q.defer()
    $http.post "#{config.url.api}/login", { name: user.name, password: user.password }
    .success (data, status, headers) ->
      token = headers('authorization')
      $http.defaults.headers.common['authorization'] = token
      if user.remember
        ipCookie('authorization', token, { expires: 180, path: '/', domain: config.host.domain })
      else
        ipCookie('authorization', token, { path: '/', domain: config.host.domain })
      deferred.resolve data
    .error (error) ->
      deferred.reject undefined
    deferred.promise

  logoff: ->
    deferred = $q.defer()
    $http.post "#{config.url.api}/logoff", undefined
    .success (data) ->
      ipCookie.remove('authorization', { path: '/' })
      delete $http.defaults.headers.common['authorization']
      deferred.resolve "OK"
    deferred.promise
]