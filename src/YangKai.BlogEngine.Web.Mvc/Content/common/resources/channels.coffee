angular.module("resource.channels", ["ngResource"])
.factory "Channel", ['$resource',($resource) ->
  $resource "/odata/Channel:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'Url' 
        $inlinecount:'allpages'
        $filter:'IsDeleted eq false'
    edit:
      method: "PUT"
    archives:
      method: "GET"
      params:
        $expand:'Groups/Posts'
        $filter:'IsDeleted eq false'
        $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/Posts/Title,Groups/Posts/Url,Groups/Posts/PubDate,Groups/Posts/IsDeleted'
]