angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['odataResource', (odataResource) ->
  odataResource "#{config.url.api}/category",
    list:
      method: "GET"
    main:
      url: "#{config.url.api}/category"
      cache: true
      method: "GET"
      params:
        $filter: 'main eq true'
]
