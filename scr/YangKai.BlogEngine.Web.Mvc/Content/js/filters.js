
angular.module("formatFilters", []).filter("jsondate", function() {
  return function(input, fmt) {
    return input.Format(fmt);
  };
}).filter("isFuture", function() {
  return function(input) {
    return new Date(input) > new Date();
  };
});
