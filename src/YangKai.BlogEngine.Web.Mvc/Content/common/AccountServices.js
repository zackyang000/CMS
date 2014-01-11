
angular.module("AccountServices", []).factory("account", [
  '$http', '$q', function($http, $q) {
    return {
      get: function() {
        var deferred;
        deferred = $q.defer();
        $http.get("/admin/getuser", {
          cache: true
        }).success(function(data) {
          return deferred.resolve(data);
        }).error(function(data) {
          return deferred.reject(data);
        });
        return deferred.promise;
      }
    };
  }
]);
