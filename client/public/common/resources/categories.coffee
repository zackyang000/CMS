angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource', ($resource) ->
  $resource "#{config.url.api}/categories/:id/:action", {id:'@id'},
    query:
      cache: true
      method: "GET"
    update:
      method: "PUT"
    main:
      cache: true
      method: "GET"
      params:
        $filter: 'main eq true'
]
