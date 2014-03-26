myDirectives.directive  "zyDropzone", ->
  link: (scope, elm, attrs, ngModel) ->

    options = scope.$eval(attrs.options)
    if (!options)
      throw "system.directives.negDropzone -> error: required attribute 'options' is not found or empty."

    url = options.url
    if (!url)
      throw "system.directives.negDropzone -> error: 'url' in attribute #{attrs.options} is not set or empty."

    maxFilesize = options.maxFilesize ? 2048
    addRemoveLinks = options.addRemoveLinks ? false
    acceptedFiles = options.acceptedFiles ? null


    $(elm).addClass("dropzone")

    $(elm).dropzone
      paramName: "file"
      maxFilesize: maxFilesize #MB
      url: url
      addRemoveLinks: addRemoveLinks
      dictDefaultMessage: "<span class=\"bigger-150 bolder\"><i class=\"icon-caret-right red\"></i>  Drop files</span> to upload 				\t\t\t<span class=\"smaller-80 grey\">(or click)</span> <br /> \t\t\t\t<i class=\"upload-icon icon-cloud-upload blue icon-3x\"></i>"
      dictResponseError: "Upload Faild!"
      acceptedFiles: acceptedFiles
      previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-details\">\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n    <div class=\"dz-size\" data-dz-size></div>\n    <img data-dz-thumbnail />\n  </div>\n  <div class=\"progress progress-small progress-striped active\"><div class=\"progress-bar progress-bar-success\" data-dz-uploadprogress></div></div>\n  <div class=\"dz-success-mark\"><span></span></div>\n  <div class=\"dz-error-mark\"><span></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n</div>"

