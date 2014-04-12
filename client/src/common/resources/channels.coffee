angular.module("resource.channels", ["ngResource"])
.factory "Channel", ['$resource',($resource) ->
  $resource "#{config.apiHostTemp}/odata/Channel:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'Url' 
        $inlinecount:'allpages'
        $filter:'IsDeleted eq false'
    queryOnce:
      cache:true
      method: "GET"
      params:
        $orderby:'Url' 
        $inlinecount:'allpages'
        $filter:'IsDeleted eq false'
    edit:
      method: "PUT"
]