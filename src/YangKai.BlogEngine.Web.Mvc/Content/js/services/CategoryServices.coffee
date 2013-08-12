angular.module("CategoryServices", ["ngResource"])
.factory "Category", ['$resource',($resource) ->
  $resource "/odata/Category:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'Url' 
        $inlinecount:'allpages'
        $filter:'IsDeleted eq false'
]