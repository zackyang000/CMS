
myDirectives.directive("whenRouteChange", [
  "$rootScope", function($rootScope) {
    return {
      link: function(scope, element, attr) {
        element.addClass('hide');
        $rootScope.$on('$routeChangeStart', function() {
          return element.removeClass('hide');
        });
        return $rootScope.$on('$routeChangeSuccess', function() {
          return element.addClass('hide');
        });
      }
    };
  }
]);
