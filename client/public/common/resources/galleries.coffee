angular.module("resource.galleries", ["ngResource"])
.factory "Galleries", ['odataResource', (odataResource) ->
  odataResource "#{config.url.api}/gallery",
    list:
      method: "GET"
      params:
        $orderby: 'date desc'
]
