
myDirectives.directive("odataPager", function($compile) {
  return function(scope, element, attrs) {
    var data, update;
    data = void 0;
    update = function() {
      if (scope.numData !== data['odata.count'] && scope.numData) {
        scope.currentPage = 1;
      }
      scope.numData = data['odata.count'];
      scope.numPages = Math.ceil(scope.numData / 10);
      element.context.innerHTML = '<pagination on-select-page="setPage(page)" num-pages="numPages" current-page="currentPage" max-size="10" boundary-links="true" rotate="false"></pagination>';
      if (scope.currentPage < scope.numPages) {
        element.context.innerHTML += '<div>{{(currentPage-1)*10+1}} - {{currentPage*10}} of {{numData}}</div>';
      } else {
        element.context.innerHTML += '<div>{{(currentPage-1)*10+1}} - {{numData}} of {{numData}}</div>';
      }
      if (scope.numData === '0') element.context.innerHTML = '';
      $compile(element.contents())(scope);
      debugger;
    };
    return scope.$watch(attrs.odataPager, function(value) {
      data = value;
      if (data === null || data === void 0) return;
      return update();
    });
  };
});
