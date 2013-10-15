myDirectives.directive "myDatepicker", ->
  restrict: "A"
  require: "ngModel"
  link: (scope, element, attrs, ngModelCtrl) ->
    $ ->
      element.attr("readonly":"readonly")
      element.datepicker
        changeMonth: true
        changeYear: true
        dateFormat: "yy/mm/dd"
        showAnim:"drop"
        onSelect: (date) ->
          ngModelCtrl.$setViewValue date
          scope.$apply()


