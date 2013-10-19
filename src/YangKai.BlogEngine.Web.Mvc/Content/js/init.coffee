#AngularJS统一异常捕获Handler
interceptor = ["$rootScope", "$q", (scope, $q) ->
  success = (response) ->
    response
  error = (response) ->
    debugger
    status = response.status
    if status is 401
      message.error '401 Unauthorized'
    else if status is 400
      message.error response.data['odata.error'].innererror.message
    else if status is 500
      message.error response.data['odata.error'].innererror.message
    $q.reject(response)
  (promise) ->
    promise.then success, error
]

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

$(document).ready ->
  #代码高亮配置
  SyntaxHighlighter.defaults['gutter'] = true;
  SyntaxHighlighter.defaults['collapse'] = false;
  SyntaxHighlighter.defaults['quick-code'] = false;
  SyntaxHighlighter.all();