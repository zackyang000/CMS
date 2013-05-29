angular.module("formatFilters", [])
.filter "jsondate", ->
  (input,fmt) ->
    input.Format(fmt)