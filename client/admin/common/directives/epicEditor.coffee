angular.module("zy.directives.epicEditor",[])

.directive 'epicEditor', ->
  require: "ngModel"
  replace: true
  template: "<div class=\"epic-editor\"></div>"
  scope:
    md: '='
  link: (scope, element, attrs, ngModel) ->
    new EpicEditor().remove('article')
    opts =
      container: element.get(0)
      file:
        name: 'article'
        defaultContent: scope.md
        autoSave: false
      autogrow:
        minHeight: 300
        maxHeight: 300
      theme:
        base: '/../../plugin/EpicEditor-v0.2.2/themes/base/epiceditor.css'
        preview: '/../../plugin/EpicEditor-v0.2.2/themes/preview/epic-light.css'
        editor: '/../../plugin/EpicEditor-v0.2.2/themes/editor/github.css'

    editor = new EpicEditor(opts)

    editor.load ->
      epicEditor = editor.getElement("editor")
      content = epicEditor.body.innerHTML

      $("body", epicEditor).blur ->
        currentContent = $(this).html()
        if content != currentContent
          content = currentContent
          editor.save()
          scope.$apply ->
            scope.md = editor.exportFile()
            htmlContent = editor.exportFile(undefined, 'html')
            ngModel.$setViewValue htmlContent