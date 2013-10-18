angular.module("PhotoServices", ["ngResource"])
.factory "Photo", ['$resource',($resource) ->
  $resource "/odata/Photo:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc'
    update:
      method: "PUT"
]