myDirectives.directive "activeLink", 
["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeLink
    path = $(element).children("a")[0].hash.substring(2)
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      if match(path,currentPath)
        element.addClass clazz
      else
        element.removeClass clazz

    match = (path,currentPath)->
      if path=='/'
        if currentPath=='/'
          return true
      else if currentPath.indexOf(path) is 0
        return true
      return false
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
    scope.$watch "location.path()", (currentPath) ->
      element.removeClass clazz
      for path in paths
        if match(path,currentPath)
          element.addClass clazz

    match = (path,currentPath)->
      if path=='/'
        if currentPath=='/'
          return true
      else if currentPath.indexOf(path) is 0
        return true
      return false
]