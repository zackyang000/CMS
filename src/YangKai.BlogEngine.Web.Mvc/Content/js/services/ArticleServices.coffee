angular.module("ArticleServices", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/odata/Post:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $expand:'Categorys,Tags,Group,Group/Channel'
        $inlinecount:'allpages'
    get:
      method: "GET"
      params:
        $expand:'Categorys,Tags,Group,Group/Channel,Comments'
    nav:
      method: "GET"
      params:
        action:"nav"
      isArray:true
    related:
      method: "GET"
      params:
        action:"related"
      isArray:true
]