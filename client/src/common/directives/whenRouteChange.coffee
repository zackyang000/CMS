angular.module("zy.directives.whenRouteChange",[])

.directive "zyWhenRouteChange",
["$rootScope",($rootScope)->
  link:(scope, element, attr) ->
    element.addClass('hide')

    $rootScope.$on '$routeChangeStart',->
      element.removeClass('hide')

    $rootScope.$on '$routeChangeSuccess',->
      element.addClass('hide')
]