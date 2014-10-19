angular.module("zy.directives.ckEditor",[])

.directive('ckEditor', ->
  require: '?ngModel'
  link: (scope, elm, attr, ngModel)->
    editor = CKEDITOR.instances[elm[0].name]
    ck = CKEDITOR.replace elm[0]
    unless ngModel then return
    ck.on 'pasteState', ->
      scope.$apply ->
        ngModel.$setViewValue ck.getData()
    ngModel.$render = (value) -> ck.setData ngModel.$viewValue
)