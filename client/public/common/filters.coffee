angular.module("zy.filters", [])

.filter("i18n", ["context",(context)->
  (input) ->
    return input  unless input
    input[context.language]
])

.filter "isFuture", ->
  (input) ->
    new Date(input) > new Date()

.filter "utc", ->
  (val) ->
    date = new Date(val)
    new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds())

.filter 'fromNow', ->
  (input) ->
    return unless input?
    return input.format()

.filter "line", ->
  (input) ->
    return unless input?
    return input.replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/\n/g,'<br />')

.filter 'fileSize', ->
  (bytes) ->
    return bytes if bytes==null or bytes==undefined
    return '' if typeof bytes isnt 'number'
    return (bytes / 1000*1000*1000).toFixed(2) + ' GB' if bytes >= 1000000000
    return (bytes / 1000*1000).toFixed(2) + ' MB' if bytes >= 1000000
    return (bytes / 1000).toFixed(2) + ' KB'

.filter 'remoteImage', ->
  (input) ->
    return unless input?
    return config.url.img + input

.filter 'gravatar', ->
  (input) ->
    if input
      return 'http://www.gravatar.com/avatar/' + md5(input)
    else
      return '/img/avatar.png'
