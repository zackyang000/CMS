angular.module("zy.directives.ngBindHtmlUnsafe",[])

.directive "ngBindHtmlUnsafe", [->
  (scope, element, attr) ->
    element.addClass("ng-binding").data "$binding", attr.ngBindHtmlUnsafe
    scope.$watch attr.ngBindHtmlUnsafe, (value) ->
      element.html value or ""
]