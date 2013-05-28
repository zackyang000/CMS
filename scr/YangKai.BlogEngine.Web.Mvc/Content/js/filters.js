
angular.module("formatFilters", []).filter("datetime", function() {
  return function(input, fmt) {
    return input.Format(fmt);
  };
});
