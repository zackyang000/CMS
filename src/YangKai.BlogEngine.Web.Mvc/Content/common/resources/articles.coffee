angular.module("resource.articles", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/odata/Article:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $expand:'Tags,Group/Channel'
        $inlinecount:'allpages'
    queryOnce:
      cache:true
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $inlinecount:'allpages'
    get:
      method: "GET"
      params:
        $top:1
    getOnce:
      cache:true
      method: "GET"
      params:
        $top:1
    update:
      method: "PUT"
    browsed:
      method: "POST"
      params:
        action:'Browsed' 
    commented:
      method: "POST"
      params:
        action:'Commented' 
]