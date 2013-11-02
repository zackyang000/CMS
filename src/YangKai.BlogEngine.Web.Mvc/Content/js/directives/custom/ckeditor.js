
angular.module("customDirectives", []).directive('ckEditor', function() {
  return {
    require: '?ngModel',
    link: function(scope, elm, attr, ngModel) {
      var ck;
      ck = CKEDITOR.replace(elm[0], {
        toolbar: 'Main'
      });
      if (!ngModel) return;
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
