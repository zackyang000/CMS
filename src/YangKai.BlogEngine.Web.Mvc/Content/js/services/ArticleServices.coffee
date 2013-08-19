angular.module("ArticleServices", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/odata/Post:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $expand:'Categorys,Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin'
        $inlinecount:'allpages'
    get:
      method: "GET"
      params:
        $top:1
        $expand:'Categorys,Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin,Comments'
    nav:
      method: "GET"
      params:
        $top:1
        $select:'Url,Title'
    related:
      method: "GET"
      params:
        action:"related"
      isArray:true
]