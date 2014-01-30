
angular.module("ChannelServices", ['resource.channels']).factory("channel", [
  '$http', '$q', "Channel", function($http, $q, Channel) {
    return {
      get: function() {
        var deferred;
        deferred = $q.defer();
        Channel.queryOnce({
          $orderby: 'OrderId',
          $expand: 'Groups',
          $select: 'Name,Url,IsDefault,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
        }, function(data) {
          return deferred.resolve(data.value);
        }, function(data) {
          return deferred.reject(data);
        });
        return deferred.promise;
      },
      getdefault: function() {
        var deferred;
        deferred = $q.defer();
        Channel.queryOnce({
          $orderby: 'OrderId',
          $expand: 'Groups',
          $select: 'Name,Url,IsDefault,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
        }, function(data) {
          var item, _i, _len, _ref, _results;
          _ref = data.value;
          _results = [];
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            item = _ref[_i];
            if (item.IsDefault) {
              deferred.resolve(item);
              break;
            } else {
              _results.push(void 0);
            }
          }
          return _results;
        }, function(data) {
          return deferred.reject(data);
        });
        return deferred.promise;
      }
    };
  }
]);
