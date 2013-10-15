
myDirectives.directive("activeLink", [
  "$location", function(location) {
    return {
      restrict: "A",
      link: function(scope, element, attrs, controller) {
        var clazz, path;
        clazz = attrs.activeLink;
        path = $(element).children("a")[0].hash.substring(2);
        scope.location = location;
        return scope.$watch("location.path()", function(newPath) {
          if (path === newPath) {
            return element.addClass(clazz);
          } else {
            return element.removeClass(clazz);
          }
        });
      }
    };
  }
]);

myDirectives.directive("activeParentLink", [
  "$location", function(location) {
    return {
      restrict: "A",
      link: function(scope, element, attrs, controller) {
        var clazz, item, links, paths, _i, _len;
        clazz = attrs.activeParentLink;
        links = $(element).children("ul").children("li").children("a");
        paths = [];
        for (_i = 0, _len = links.length; _i < _len; _i++) {
          item = links[_i];
          paths.push(item.hash.substring(2));
        }
        scope.location = location;
        return scope.$watch("location.path()", function(newPath) {
          var path, _j, _len1, _results;
          element.removeClass(clazz);
          _results = [];
          for (_j = 0, _len1 = paths.length; _j < _len1; _j++) {
            path = paths[_j];
            if (path === newPath) {
              _results.push(element.addClass(clazz));
            } else {
              _results.push(void 0);
            }
          }
          return _results;
        });
      }
    };
  }
]);
