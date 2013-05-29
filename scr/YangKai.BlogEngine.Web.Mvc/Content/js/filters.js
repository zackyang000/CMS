
angular.module("formatFilters", []).filter("jsondate", function() {
  return function(input, fmt) {
    return input.Format(fmt);
  };
});
