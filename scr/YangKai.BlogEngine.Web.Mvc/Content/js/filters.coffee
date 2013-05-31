angular.module("formatFilters", [])
.filter "jsondate", ->
  (input,fmt) ->
    input.Format(fmt)
.filter "isFuture", ->
  (input) ->
    new Date(input)>new Date()