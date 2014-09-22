angular.module("resource.categories", ["ngResource"])
.factory "Categories", ['$resource', ($resource) ->
  $resource "#{config.apiHost}/categories/:id/:action", {id:'@id'},
    query:
      method: "GET"
    update:
      method: "PUT"
    main:
      method: "GET"
      params:
        $filter: 'main eq true'
]
