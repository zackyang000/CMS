angular.module("zy.directives.fancybox",[])

.directive "fancybox", ($compile, $timeout) ->
  link: (scope, element, attrs) ->
    if scope.$last
      setTimeout ->
        $('.fancybox-button').fancybox
          groupAttr: 'data-rel'
          prevEffect: 'none'
          nextEffect: 'none'
          closeBtn: true
          helpers:
            title:
              type: 'over'
        , 0
