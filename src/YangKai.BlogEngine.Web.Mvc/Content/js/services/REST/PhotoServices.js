
angular.module("PhotoServices", ["ngResource"]).factory("Photo", [
  '$resource', function($resource) {
    return $resource("/odata/Photo:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'CreateDate desc'
        }
      },
      update: {
        method: "PUT"
      }
    });
  }
]);
