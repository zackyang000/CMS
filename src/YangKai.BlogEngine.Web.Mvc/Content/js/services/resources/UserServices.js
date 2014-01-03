
angular.module("UserServices", ["ngResource"]).factory("User", [
  '$resource', function($resource) {
    return $resource("/odata/User:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      signin: {
        method: "POST",
        params: {
          action: 'Signin'
        }
      },
      signout: {
        method: "POST",
        params: {
          action: 'Signout'
        }
      }
    });
  }
]);
