
angular.module("IssueServices", ["ngResource"]).factory("Issue", [
  '$resource', function($resource) {
    return $resource("/odata/Issue:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'CreateDate desc',
          $inlinecount: 'allpages'
        }
      },
      update: {
        method: "PUT"
      }
    });
  }
]);
