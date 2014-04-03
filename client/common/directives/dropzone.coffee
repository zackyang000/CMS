angular.module("zy.directives.dropzone",[])

.directive("zyDropzone", ->
  link: (scope, elm, attrs, ngModel) ->

    options = scope.$eval(attrs.options)

    if (!options)
      throw "system.directives.negDropzone -> error: required attribute 'options' is not found or empty."
    if (!options.url)
      throw "system.directives.negDropzone -> error: 'url' in attribute #{attrs.options} is not set or empty."

    $(elm).addClass("dropzone")

    $(elm).dropzone
      paramName: "file"
      maxFilesize: options.maxFilesize || 2048 #MB
      url: options.url
      addRemoveLinks: options.addRemoveLinks || false
      dictDefaultMessage: "<span class=\"bigger-150 bolder\"><i class=\"icon-caret-right red\"></i>  Drop files</span> to upload 				\t\t\t<span class=\"smaller-80 grey\">(or click)</span> <br /> \t\t\t\t<i class=\"upload-icon icon-cloud-upload blue icon-3x\"></i>"
      dictResponseError: "Upload Faild!"
      acceptedFiles: options.acceptedFiles || null
      previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-details\">\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n    <div class=\"dz-size\" data-dz-size></div>\n    <img data-dz-thumbnail />\n  </div>\n  <div class=\"progress progress-small progress-striped active\"><div class=\"progress-bar progress-bar-success\" data-dz-uploadprogress></div></div>\n  <div class=\"dz-success-mark\"><span></span></div>\n  <div class=\"dz-error-mark\"><span></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n</div>"
)