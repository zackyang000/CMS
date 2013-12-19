angular.module("ArticleServices", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/odata/Article:id/:action", {id:'@id',action:'@action'},
    query:
      method: "GET"
      params:
        $orderby:'CreateDate desc' 
        $expand:'Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin,QrCode'
        $inlinecount:'allpages'
    get:
      method: "GET"
      params:
        $top:1
        $expand:'Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin,QrCode,Comments'
    update:
      method: "PUT"
    nav:
      method: "GET"
      params:
        $top:1
        $select:'Url,Title'
    related:
      method: "GET"
      params:
        $top:8
        $select:'Url,Title,PubDate'
    browsed:
      method: "POST"
      params:
        action:'Browsed' 
    commented:
      method: "POST"
      params:
        action:'Commented' 
]