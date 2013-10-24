angular.module("formatFilters", [])
.filter "isFuture", ->
  (input) ->
    new Date(input)>new Date()
.filter "line", ->
  (input) ->
    return input if !input
    return input.replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/\n/g,'<br />')
#转换文件size单位
.filter 'formatFileSize', ->
  (bytes) ->
    return bytes if bytes==null or bytes==undefined
    return '' if typeof bytes isnt 'number'
    return (bytes / 1000000000).toFixed(2) + ' GB' if bytes >= 1000000000
    return (bytes / 1000000).toFixed(2) + ' MB' if bytes >= 1000000
    (bytes / 1000).toFixed(2) + ' KB'
#在URL不完整时不显示
.filter 'image', ->
  (url) ->
    return '' if url.charAt(url.length - 1) is '/'
    return url