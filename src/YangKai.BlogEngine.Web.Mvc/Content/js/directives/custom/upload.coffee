myDirectives.directive "upload", ["uploadManager", factory = (uploadManager) ->
  restrict: "A"
  link: (scope, element, attrs) ->
    $(element).fileupload
      dataType: "text"
      add: (e, data) ->
        uploadManager.add data

      progressall: (e, data) ->
        progress = parseInt(data.loaded / data.total * 100, 10)
        uploadManager.setProgress progress

      done: (e, data) ->
        uploadManager.setFileStatus data
]


myDirectives.directive "dynamicUpload", ($compile)->
  (scope, element, attrs) ->
    update =(value) ->
      element.context.innerHTML="<input type='file' name='file' data-url='fileupload/upload/#{value}' upload />"
      $compile(element.contents())(scope)

    scope.$watch attrs.dynamicUpload, (value) ->
      update('1')


angular.module("FileUpload",[])
.factory "uploadManager", ($rootScope) ->
  _files = []
  add: (file) ->
    _files.push file
    $rootScope.$broadcast "fileAdded", file.files[0]

  cancel: (file)->
    deleteFile=f for f in _files when f.files[0].name is file.name
    _files.splice(_files.indexOf(deleteFile),1)

  clear: ->
    _files = []

  files: ->
    fileNames = []
    $.each _files, (index, file) ->
      fileNames.push file.files[0].name
    fileNames

  upload: ->
    $.each _files, (index, file) ->
      file.submit()
    @clear()

  setProgress: (percentage) ->
    $rootScope.$broadcast "uploadProgress", percentage

  setFileStatus: (data) ->
    $rootScope.$broadcast "fileUploaded", data