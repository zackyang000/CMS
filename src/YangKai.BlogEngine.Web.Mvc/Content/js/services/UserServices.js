
angular.module("UserServices", ["ngResource"]).factory("User", [
  '$resource', function($resource) {
    return $resource("/odata/User:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      login: {
        method: "POST",
        params: {
          action: 'Login'
        }
      }
    });
  }
]);
