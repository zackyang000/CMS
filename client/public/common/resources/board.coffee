angular.module("resource.board", ["ngResource"])
.factory "Board", ['odataResource', (odataResource) ->
  odataResource "#{config.url.api}/board",
    list:
      method: "GET"
      params:
        $orderby: 'date desc'
    update:
      method: "PUT"
]
