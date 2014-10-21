galleryInit = ->
  setTimeout (->
    $("[data-rel=\"colorbox\"]").colorbox
      reposition: true
      scalePhotos: true
      scrolling: false
      previous: "<i class=\"fa fa-arrow-left\"></i>"
      next: "<i class=\"fa fa-arrow-right\"></i>"
      close: "&times;"
      current: "{current} of {total}"
      maxWidth: "100%"
      maxHeight: "100%"
      onOpen: ->
        document.body.style.overflow = "hidden"
      onClosed: ->
        document.body.style.overflow = "auto"
      onComplete: ->
        $.colorbox.resize()
  ), 1000

#代码高亮
codeformat = ->
  jQuery ->
    #SyntaxHighlighter.highlight()

#fix Array indexOf() in JavaScript for IE browsers
unless Array::indexOf
  Array::indexOf = (elt) -> #, from
    len = @length >>> 0
    from = Number(arguments_[1]) or 0
    from = (if (from < 0) then Math.ceil(from) else Math.floor(from))
    from += len  if from < 0
    while from < len
      return from  if from of this and this[from] is elt
      from++
    -1
