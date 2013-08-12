
angular.module("CategoryServices", ["ngResource"]).factory("Category", [
  '$resource', function($resource) {
    return $resource("/odata/Category:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'Url',
          $inlinecount: 'allpages',
          $filter: 'IsDeleted eq false'
        }
      }
    });
  }
]);
