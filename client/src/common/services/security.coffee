angular.module("zy.services.security", ['resource.users'])
.factory "security", ['User','$q', "$http", (User, $q, $http) ->
  autoLogin: ->
    self = this
    deferred = $q.defer()
    token=$.cookie('authorization')
    if token
      $http.defaults.headers.common['authorization']=token
      User.autoSignin {id:'(1)'}, null
      ,(data)->
        self.account = data
        deferred.resolve "OK"
      ,(error)->
        deferred.reject "Failed."
    else
      deferred.reject "Failed."
    deferred.promise

  login: (user) ->
    self = this
    deferred = $q.defer()
    $http.defaults.headers.common['authorization']=''
    User.signin {id:'(1)'}, user
    ,(data, headers)->
      if user.IsRemember
        $.cookie('authorization', headers('authorization'), {expires: 180, path: '/'})
      else
        $.cookie('authorization', headers('authorization'), { path: '/'})
      self.account = data
      deferred.resolve "OK"
    ,(error)->
      deferred.reject "Username or password wrong."
    deferred.promise
  logoff: ->
    self = this
    deferred = $q.defer()
    User.signout {id:'(1)'}
    ,(data)->
      $.removeCookie('authorization')
      delete $http.defaults.headers.common['authorization']
      self.account = undefined
      deferred.resolve "OK"
    deferred.promise
]