
angular.module("formatFilters", []).filter("jsondate", function() {
  return function(input, fmt) {
    return input.Format(fmt);
  };
}).filter("isFuture", function() {
  return function(input) {
    return new Date(input) > new Date();
  };
}).filter('formatFileSize', function() {
  return function(bytes) {
    if (bytes === null || bytes === void 0) return bytes;
    if (typeof bytes !== 'number') return '';
    if (bytes >= 1000000000) return (bytes / 1000000000).toFixed(2) + ' GB';
    if (bytes >= 1000000) return (bytes / 1000000).toFixed(2) + ' MB';
    return (bytes / 1000).toFixed(2) + ' KB';
  };
});
