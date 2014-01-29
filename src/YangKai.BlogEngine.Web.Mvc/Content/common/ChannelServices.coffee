angular.module("ChannelServices", ['resource.channels'])
.factory "channel", ['$http','$q',"Channel",($http,$q,Channel) ->
  get: ->
    deferred = $q.defer()
    Channel.queryOnce 
      $orderby:'OrderId' 
      $expand:'Groups'
      $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    ,(data)->
      deferred.resolve data.value
    ,(data)->
      deferred.reject data
    deferred.promise
]