myDirectives.directive "odataPager", ($compile)->
  
  (scope, element, attrs) ->
    data=undefined

    update=->
      scope.currentPage=1 if scope.numData isnt data['odata.count'] #如果数量发生变化则重置为第一页.
      scope.numData=data['odata.count']
      scope.numPages=Math.ceil(scope.numData / 10)
      element.context.innerHTML='<pagination on-select-page="setPage(page)" num-pages="numPages" current-page="currentPage" max-size="10" boundary-links="true" rotate="false"></pagination>'
      if scope.currentPage<scope.numPages
        element.context.innerHTML+='<div>{{(currentPage-1)*10+1}} - {{currentPage*10}} of {{numData}}</div>'
      else
        element.context.innerHTML+='<div>{{(currentPage-1)*10+1}} - {{numData}} of {{numData}}</div>'

      $compile(element.contents())(scope)

    scope.$watch attrs.odataPager, (value) ->
      data=value
      return if data==null or data==undefined
      update()
    