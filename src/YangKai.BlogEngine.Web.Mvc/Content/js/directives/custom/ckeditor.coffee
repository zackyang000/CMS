myDirectives.directive 'ckEditor', ->
  require: '?ngModel'
  link: (scope, elm, attr, ngModel)->
    ck = CKEDITOR.replace elm[0], { toolbar: 'Main' }
    unless ngModel then return
    ck.on 'pasteState', ->
      scope.$apply ->
        ngModel.$setViewValue ck.getData()
    ngModel.$render = (value)-> ck.setData ngModel.$viewValue