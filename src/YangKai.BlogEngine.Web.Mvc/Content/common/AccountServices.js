
angular.module("AccountServices", []).factory("account", [
  '$http', '$q', function($http, $q) {
    return {
      get: function() {
        var deferred, self;
        deferred = $q.defer();
        if (this.data) {
          deferred.resolve(this.data);
        } else {
          self = this;
          $http.get("/admin/getuser").success(function(data) {
            self.data = data;
            return deferred.resolve(self.data);
          }).error(function(data) {
            return deferred.reject(data);
          });
        }
        return deferred.promise;
      }
    };
  }
]);
