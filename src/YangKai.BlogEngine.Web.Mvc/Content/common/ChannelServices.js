
angular.module("ChannelServices", ['resource.channels']).factory("channel", [
  '$http', '$q', "Channel", function($http, $q, Channel) {
    return {
      get: function() {
        var deferred;
        deferred = $q.defer();
        Channel.queryOnce({
          $orderby: 'OrderId',
          $expand: 'Groups',
          $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
        }, function(data) {
          return deferred.resolve(data.value);
        }, function(data) {
          return deferred.reject(data);
        });
        return deferred.promise;
      }
    };
  }
]);
