
myDirectives.directive("ngBindHtmlUnsafe", [
  function() {
    return function(scope, element, attr) {
      var ngBindHtmlUnsafeWatchAction;
      element.addClass("ng-binding").data("$binding", attr.ngBindHtmlUnsafe);
      return scope.$watch(attr.ngBindHtmlUnsafe, ngBindHtmlUnsafeWatchAction = function(value) {
        return element.html(value || "");
      });
    };
  }
]);
