angular.module("ChannelServices", ['resource.channels'])
.factory "channel", ['$http','$q',"Channel",($http,$q,Channel) ->
  get: ->
    deferred = $q.defer()
    Channel.queryOnce 
      $orderby:'OrderId' 
      $expand:'Groups'
      $select:'Name,Url,IsDefault,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    ,(data)->
      deferred.resolve data.value
    ,(data)->
      deferred.reject data
    deferred.promise
  
  getdefault: ->
    deferred = $q.defer()
    Channel.queryOnce 
      $orderby:'OrderId' 
      $expand:'Groups'
      $select:'Name,Url,IsDefault,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/OrderId'
    ,(data)->
      for item in data.value
        if item.IsDefault
          deferred.resolve item
          break
    ,(data)->
      deferred.reject data
    deferred.promise
]