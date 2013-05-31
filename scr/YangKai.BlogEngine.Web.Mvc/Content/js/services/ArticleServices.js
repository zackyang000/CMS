
angular.module("ArticleServices", ["ngResource"]).factory("Article", [
  '$resource', function($resource) {
    return $resource("/api/article/:id", {
      id: '@id'
    }, {
      querybypaged: {
        method: "GET"
      }
    });
  }
]);
