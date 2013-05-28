angular.module("formatFilters", [])
.filter "datetime", ->
  (input,fmt) ->
    input.Format(fmt)