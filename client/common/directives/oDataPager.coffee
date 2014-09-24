angular.module("zy.directives.odataPager",[])

.directive("zyOdataPager", ['$compile',($compile)->
  link:(scope, element, attrs) ->
    data=undefined
    update = ->
      scope.currentPage=1 if scope.currentPage is undefined
      scope.currentPage=1 if scope.numData isnt data['@odata.count'] and scope.numData #如果数量发生变化则重置为第一页.
      scope.numData=data['@odata.count']
      debugger
      scope.numPages=Math.ceil(scope.numData / 10)
      scope.totalItems=scope.numData
      element.context.innerHTML='<pagination on-select-page="setPage(page)" total-items="totalItems" page="currentPage" max-size="10" boundary-links="true" rotate="false"></pagination>'
      if scope.currentPage < scope.numPages
        element.context.innerHTML+='<div>{{(currentPage-1)*10+1}} - {{currentPage*10}} of {{numData}}</div>'
      else
        element.context.innerHTML+='<div>{{(currentPage-1)*10+1}} - {{numData}} of {{numData}}</div>'
      element.context.innerHTML='' if scope.numData is '0'
      $compile(element.contents())(scope)

    scope.$watch attrs.zyOdataPager, (value) ->
      data=value
      return if data==null or data==undefined
      update()
])