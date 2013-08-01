angular.module("ArticleServices", ["ngResource"])
.factory "Article", ['$resource',($resource) ->
  $resource "/api/post/:id", {id:'@id'},
    query:
      method: "GET"
      params:
        $top:10
        $orderby:'CreateDate desc' 
        $expand:'Categorys,Tags,Group'
      isArray:true
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