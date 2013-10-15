myDirectives.directive "activeLink", ["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeLink
    path = $(element).children("a")[0].hash.substring(2)
    scope.location = location
    scope.$watch "location.path()", (newPath) ->
      if path is newPath
        element.addClass clazz
      else
        element.removeClass clazz
]

myDirectives.directive "activeParentLink", ["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeParentLink
    links = $(element).children("ul").children("li").children("a")
    paths=[]
    for item in links
      paths.push item.hash.substring(2)
    scope.location = location
    scope.$watch "location.path()", (newPath) ->
      element.removeClass clazz
      for path in paths
        if path is newPath
          element.addClass clazz
]