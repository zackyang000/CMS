angular.module("resource.articles", ["ngResource"])
.factory "Articles", ['odataResource', (odataResource) ->
  odataResource "#{config.url.api}/article",
    list:
      method: "GET"
      params:
        $orderby: 'date desc'
    addComment:
      method: "POST"
      params:
        action: "add-comment"
    browsed:
      method: "POST"
      params:
        action: 'browsed'
]
