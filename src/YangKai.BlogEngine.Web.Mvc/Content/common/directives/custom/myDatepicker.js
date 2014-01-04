

myDirectives.directive("myDatepicker", function() {
  return {
    restrict: "A",
    require: "ngModel",
    link: function(scope, element, attrs, ngModelCtrl) {
      return $(function() {
        element.attr({
          "readonly": "readonly"
        });
        return element.datepicker({
          changeMonth: true,
          changeYear: true,
          dateFormat: "yy/mm/dd",
          showAnim: "drop",
          onSelect: function(date) {
            ngModelCtrl.$setViewValue(date);
            return scope.$apply();
          }
        });
      });
    }
  };
});
