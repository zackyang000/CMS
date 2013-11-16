
myDirectives.directive("activeLink", [
  "$location", function(location) {
    return {
      restrict: "A",
      link: function(scope, element, attrs, controller) {
        var clazz, match, path;
        clazz = attrs.activeLink;
        path = $($(element).children("a")[0]).attr('href');
        scope.location = location;
        scope.$watch("location.path()", function(currentPath) {
          if (match(path, currentPath)) {
            return element.addClass(clazz);
          } else {
            return element.removeClass(clazz);
          }
        });
        return match = function(path, currentPath) {
          if (path === '/admin') {
            return currentPath === '/admin';
          } else {
            return currentPath.indexOf(path) === 0;
          }
        };
      }
    };
  }
]);

myDirectives.directive("activeParentLink", [
  "$location", function(location) {
    return {
      restrict: "A",
      link: function(scope, element, attrs, controller) {
        var clazz, item, links, match, paths, _i, _len;
        clazz = attrs.activeParentLink;
        links = $(element).children("a").next().children("li").children("a");
        paths = [];
        for (_i = 0, _len = links.length; _i < _len; _i++) {
          item = links[_i];
          paths.push($(item).attr('href'));
        }
        scope.location = location;
        scope.$watch("location.path()", function(currentPath) {
          var path, _j, _len1, _results;
          element.removeClass(clazz);
          _results = [];
          for (_j = 0, _len1 = paths.length; _j < _len1; _j++) {
            path = paths[_j];
            if (match(path, currentPath)) {
              _results.push(element.addClass(clazz));
            } else {
              _results.push(void 0);
            }
          }
          return _results;
        });
        return match = function(path, currentPath) {
          if (path === '/admin') {
            return currentPath === '/admin';
          } else {
            return currentPath.indexOf(path) === 0;
          }
        };
      }
    };
  }
]);
