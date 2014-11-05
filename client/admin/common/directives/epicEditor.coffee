angular.module("zy.directives.epicEditor",[])

.directive 'epicEditor', ->
  require: "ngModel"
  replace: true
  template: "<div class=\"epic-editor\"></div>"
  link: (scope, element, attrs, ngModel) ->
    opts =
      container: element.get(0)
      file:
        autoSave: false
      theme:
        base: '/../../plugin/EpicEditor-v0.2.2/themes/base/epiceditor.css'
        preview: '/../../plugin/EpicEditor-v0.2.2/themes/preview/epic-light.css'
        editor: '/../../plugin/EpicEditor-v0.2.2/themes/editor/github.css'

    editor = new EpicEditor(opts)

    editor.load ->
      iFrameEditor = editor.getElement("editor")
      contents = $("body", iFrameEditor).html()

      $("body", iFrameEditor).blur ->
        unless contents is $(this).html()
          contents = $(this).html()
          editor.save()
          rawContent = editor.exportFile(undefined, 'html')
          ngModel.$setViewValue rawContent
          scope.$apply()
