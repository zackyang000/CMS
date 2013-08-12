angular.module("ChannelServices", ["ngResource"])
.factory "Channel", ['$resource',($resource) ->
  $resource "/odata/Channel:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'Url' 
        $inlinecount:'allpages'
        $filter:'IsDeleted eq false'
]