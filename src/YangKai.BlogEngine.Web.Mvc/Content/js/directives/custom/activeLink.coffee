myDirectives.directive "activeLink", 
["$location", (location) ->
  restrict: "A"
  link: (scope, element, attrs, controller) ->
    clazz = attrs.activeLink
    path = $($(element).children("a")[0]).attr('href')
    scope.location = location
    scope.$watch "location.path()", (currentPath) ->
      if match(path,currentPath)
        element.addClass clazz
      else
        element.removeClass clazz

    match = (path,currentPath)->
      if path=='/admin'
        return currentPath=='/admin'
      else
        return currentPath.indexOf(path) is 0
]

myDirectives.directive "activeParentLink", ["$location", (location) ->
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
        if match(path,currentPath)
          element.addClass clazz

    match = (path,currentPath)->
      if path=='/admin'
        return currentPath=='/admin'
      else
        return currentPath.indexOf(path) is 0
]