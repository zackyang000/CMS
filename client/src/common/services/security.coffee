angular.module("zy.services.security", ['resource.users'])
.factory "security", ['User','$q', "$httpProvider",(User, $q, $httpProvider) ->
  autoLogin: ->
    self = this
    deferred = $q.defer()
    token=$.cookie('x-security-token')
    if token
      $httpProvider.defaults.headers.common['x-security-token']=token
      User.autoSignin {id:'(1)'}, null
      ,(data)->
        if data
          self.account = data
          deferred.resolve "OK"
        else
          deferred.reject "Failed."
      ,(error)->
        deferred.reject "Failed."
    else
      deferred.reject "Failed."
    deferred.promise

  login: (user) ->
    self = this
    deferred = $q.defer()
    User.signin {id:'(1)'}, user
    ,(data, status, headers, config)->
      if user.IsRememberMe
        $.cookie('x-security-token', headers('x-security-token'), {expires: 180, path: '/'})
      else
        $.cookie('x-security-token', headers('x-security-token'), { path: '/'})
      self.account = data
      deferred.resolve "OK"
    ,(error)->
      deferred.reject "Failed."

  logoff: ->
    self = this
    deferred = $q.defer()
    User.signout {id:'(1)'}
    ,(data)->
      $.removeCookie('x-security-token')
      delete $http.defaults.headers.common['x-security-token']
      self.account = undefined
      deferred.resolve "OK"
]