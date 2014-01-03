
angular.module("VersionServices", []).factory("VersionService", [
  '$http', '$q', function($http, $q) {
    return {
      get: function() {
        var deferred, self;
        deferred = $q.defer();
        if (this.data) {
          deferred.resolve(this.data);
        } else {
          self = this;
          $http.get("/version.txt").success(function(data) {
            var begin, info, item, list, versions, _i, _len;
            list = data.match(/([^\r\n])+/g);
            versions = [];
            for (_i = 0, _len = list.length; _i < _len; _i++) {
              item = list[_i];
              if (item.indexOf('v') === 0) {
                info = item.split(' ');
                if (info.length === 2) {
                  begin = true;
                  versions.push({
                    ver: info[0],
                    date: info[1],
                    content: []
                  });
                }
              } else {
                if (begin) {
                  versions[versions.length - 1].content.push(item);
                }
              }
            }
            self.data = versions;
            return deferred.resolve(self.data);
          }, function(data) {
            return deferred.reject(data);
          });
        }
        return deferred.promise;
      }
    };
  }
]);
