
angular.module("MessageServices", ["ngResource"]).factory("Message", [
  '$resource', function($resource) {
    return $resource("/odata/Board:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: "CreateDate desc"
        }
      },
      remove: {
        method: "POST",
        params: {
          action: 'Remove'
        }
      },
      recover: {
        method: "POST",
        params: {
          action: 'Recover'
        }
      }
    });
  }
]);
