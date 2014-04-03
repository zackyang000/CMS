angular.module("zy.filters", [])

.filter "isFuture", ->
  (input) ->
    new Date(input)>new Date()

.filter 'fromNow', ->
  (input) ->
    return unless input?
    return moment(input).fromNow()
    
.filter "line", ->
  (input) ->
    return unless input?
    return input.replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/\n/g,'<br />')

#转换文件size单位
.filter 'fileSize', ->
  (bytes) ->
    return bytes if bytes==null or bytes==undefined
    return '' if typeof bytes isnt 'number'
    return (bytes / 1000000000).toFixed(2) + ' GB' if bytes >= 1000000000
    return (bytes / 1000000).toFixed(2) + ' MB' if bytes >= 1000000
    return (bytes / 1000).toFixed(2) + ' KB'

.filter 'remoteImage', ->
  (input) ->
    return unless input?
    return config.imgHost + input