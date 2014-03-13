myDirectives.directive "whenRouteChange", 
["$rootScope",($rootScope)->
  link:(scope, element, attr) ->
    element.addClass('hide')

    $rootScope.$on '$routeChangeStart',->
      element.removeClass('hide')

    $rootScope.$on '$routeChangeSuccess',->
      element.addClass('hide')
]