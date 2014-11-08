angular.module("zy.directives.activeLink",[])

.directive("activeLink",
["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs) ->
    className = attrs.activeLink
    link = $(element).children("a")[0]
    path = $(link).attr('href')
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      if match(path, currentPath)
        element.addClass className
      else
        element.removeClass className

    match = (path, currentPath)->
      if path is '/'
        return currentPath is '/'
      else
        return currentPath.indexOf(path) is 0
])

.directive("activeParentLink", ["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs) ->
    className = attrs.activeParentLink
    links = $(element).children("ul").children("li").children("a")
    paths=[]
    for item in links
      paths.push $(item).attr('href')
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      element.removeClass className
      for path in paths
        if match(path, currentPath)
          element.addClass className

    match = (path, currentPath)->
      if path is '/'
        return currentPath is '/'
      else
        return currentPath.indexOf(path) is 0
])
