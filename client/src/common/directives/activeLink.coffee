angular.module("zy.directives.activeLink",[])

.directive("activeLink",
["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeLink
    link = $(element).children("a")[0]
    path = $(link).attr('href')
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      debugger
      if match(path,currentPath.substr(1))
        element.addClass clazz
      else
        element.removeClass clazz

    match = (path,currentPath)->
      if path=='#'
        return currentPath==''
      else
        return currentPath.indexOf(path) is 0
])

.directive("activeParentLink", ["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeParentLink
    links = $(element).children("a").next().children("li").children("a")
    paths=[]
    for item in links
      paths.push $(item).attr('href')
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      element.removeClass clazz
      for path in paths
        if match(path,currentPath.substr(1))
          element.addClass clazz

    match = (path,currentPath)->
      if path=='#'
        return currentPath==''
      else
        return currentPath.indexOf(path) is 0
])