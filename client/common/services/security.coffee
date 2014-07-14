angular.module("zy.services.security", ['resource.users'])
.factory "security", ['Users','$q', "$http", (Users, $q, $http) ->
  autoLogin: ->
    deferred = $q.defer()
    token = $.cookie('authorization')
    if token
      $http.defaults.headers.common['authorization']=token
      Users.autoSignin {id: 'user'}, {}
      ,(data)->
        deferred.resolve data
      ,(error)->
        deferred.reject undefined
    else
      deferred.reject undefined
    deferred.promise

  login: (user) ->
    deferred = $q.defer()
    Users.signin {id: 'user'}, user
    ,(data, headers)->
      if user.IsRemember
        $.cookie('authorization', headers('authorization'), {expires: 180, path: '/'})
      else
        $.cookie('authorization', headers('authorization'), { path: '/'})
      deferred.resolve data
    ,(error)->
      deferred.reject undefined
    deferred.promise

  logoff: ->
    deferred = $q.defer()
    Users.signout {id: 'user'}
    ,(data)->
      $.removeCookie('authorization', { path: '/' })
      delete $http.defaults.headers.common['authorization']
      deferred.resolve "OK"
    deferred.promise
]