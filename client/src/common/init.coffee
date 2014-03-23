uploadInit = (url) ->
  try
    $(".dropzone").dropzone
      paramName: "file"
      maxFilesize: 100 # MB
      url: url
      addRemoveLinks: false
      dictDefaultMessage: "<span class=\"bigger-150 bolder\"><i class=\"icon-caret-right red\"></i>  Drop files</span> to upload 				\t\t\t<span class=\"smaller-80 grey\">(or click)</span> <br /> \t\t\t\t<i class=\"upload-icon icon-cloud-upload blue icon-3x\"></i>"
      dictResponseError: "Upload Faild!"
      acceptedFiles: "image/*"
      previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-details\">\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n    <div class=\"dz-size\" data-dz-size></div>\n    <img data-dz-thumbnail />\n  </div>\n  <div class=\"progress progress-small progress-striped active\"><div class=\"progress-bar progress-bar-success\" data-dz-uploadprogress></div></div>\n  <div class=\"dz-success-mark\"><span></span></div>\n  <div class=\"dz-error-mark\"><span></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n</div>"

galleryInit = ->
  setTimeout (->
    $("[data-rel=\"colorbox\"]").colorbox
      reposition: true
      scalePhotos: true
      scrolling: false
      previous: "<i class=\"icon-arrow-left\"></i>"
      next: "<i class=\"icon-arrow-right\"></i>"
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

#json日期转换为Date对象
ConvertJsonDate = (jsondate) ->
  jsondate = jsondate.replace("/Date(", "").replace(")/", "")
  if jsondate.indexOf("+") > 0
    jsondate = jsondate.substring(0, jsondate.indexOf("+"))
  else jsondate = jsondate.substring(0, jsondate.indexOf("-"))  if jsondate.indexOf("-") > 0
  new Date(parseInt(jsondate, 10))

#代码高亮
codeformat = ->
  jQuery ->
    SyntaxHighlighter.highlight()

#alert方法
message =
  success: (msg) ->
    Messenger().post
      message: msg
      type: "success"
      showCloseButton: true
  error: (msg) ->
    Messenger().post
      message: msg
      type: "error"
      showCloseButton: true
      delay: 60
  confirm: (callback) ->
    msg = Messenger().post(
      message: "Do you want to continue?"
      id: "Only-one-message"
      showCloseButton: true
      actions:
        OK:
          label: "OK"
          phrase: "Confirm"
          delay: 60
          action: ->
            callback()
            msg.cancel()
        cancel:
          action: ->
            msg.cancel()
    )

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

$(document).ready ->
  #代码高亮配置
  SyntaxHighlighter.defaults['gutter'] = true;
  SyntaxHighlighter.defaults['collapse'] = false;
  SyntaxHighlighter.defaults['quick-code'] = false;
  SyntaxHighlighter.all();