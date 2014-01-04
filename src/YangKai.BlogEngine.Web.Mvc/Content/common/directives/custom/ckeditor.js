
myDirectives.directive('ckEditor', function() {
  return {
    require: '?ngModel',
    link: function(scope, elm, attr, ngModel) {
      debugger;
      var ck, editor;
      editor = CKEDITOR.instances[elm[0].name];
      if (editor) {
        editor.destroy(true);
      }
      ck = CKEDITOR.replace(elm[0], {
        toolbar: 'Main'
      });
      if (!ngModel) {
        return;
      }
      ck.on('pasteState', function() {
        return scope.$apply(function() {
          return ngModel.$setViewValue(ck.getData());
        });
      });
      return ngModel.$render = function(value) {
        return ck.setData(ngModel.$viewValue);
      };
    }
  };
});
