angular.module("resource.issues", ["ngResource"])
.factory "Issue", ['$resource',($resource) ->
  $resource "#{config.apiHostTemp}/odata/Issue:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $inlinecount:'allpages'
    update:
      method: "PUT"
]