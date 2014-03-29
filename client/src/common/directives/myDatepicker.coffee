angular.module("zy.directives.datepicker",[])

.directive("zyDatepicker", ->
  restrict: "A"
  require: "ngModel"
  link: (scope, element, attrs, ngModelCtrl) ->
    element.attr("readonly":"readonly")
    element.css("cursor","pointer")
    element.css("background","#FFF")

    options = {}
    options = $.parseJSON(attrs.negDatePicker) if attrs.negDatePicker

    element.datepicker
      changeMonth: options.changeMonth || false
      changeYear: options.changeYear || false
      dateFormat: options.dateFormat ||  "mm/dd/yy"
      showAnim: "drop"
      onSelect: (date) ->
        ngModelCtrl.$setViewValue date
        scope.$apply()
)