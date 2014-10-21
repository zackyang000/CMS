angular.module("resource.articles", ["ngResource"])
.factory "Articles", ['$resource', ($resource) ->
  $resource "#{config.url.api}/articles/:id/:action", {id:'@id'},
    query:
      method: "GET"
      params:
        $orderby: 'date desc'
    update:
      method: "PUT"
    addComment:
      method: "POST"
      params:
        action: "add-comment"
    browsed:
      method: "POST"
      params:
        action: 'browsed'
]