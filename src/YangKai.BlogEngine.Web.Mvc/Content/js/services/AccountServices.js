
angular.module("AccountServices", []).factory("account", [
  '$http', '$rootScope', function($http, $rootScope) {
    return $http.get("/admin/getuser").success(function(data) {
      return $rootScope.User = data;
    });
  }
]);
