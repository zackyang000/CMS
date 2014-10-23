angular.module("zy.directives.odataPager",['template/odata-pager.html'])

.directive("zyOdataPager", ['$compile', '$http', '$templateCache', ($compile, $http, $templateCache) ->
  link:(scope, element, attrs) ->
    data=undefined
    update = ->
      scope.currentPage = 1 if scope.currentPage is undefined
      scope.currentPage = 1 if scope.numData isnt data['@odata.count'] and scope.numData #如果数量发生变化则重置为第一页.
      scope.numData = data['@odata.count']
      scope.numPages = Math.ceil(scope.numData / 10)
      scope.totalItems = scope.numData
      scope.selectPage = (count) ->
        scope.currentPage = count
        scope.setPage(count) if scope.setPage
      $http.get("template/odata-pager.html", {cache: $templateCache}).success (tplContent) ->
         element.replaceWith($compile(tplContent.trim())(scope));

    scope.$watch attrs.zyOdataPager, (value) ->
      data=value
      return if data==null or data==undefined
      update()
])

angular.module("template/odata-pager.html", []).run ["$templateCache", ($templateCache) ->
  $templateCache.put "template/odata-pager.html",
    "<div class=\"odata-pager\">\n" +
    "  <ul class=\"pagination\">"+
    "    <li style='float: left;' ng-if='currentPage!=numPages'><a href='Javascript:void(0)' ng-click='selectPage(currentPage+1)'>Older</a></li>" +
    "    <li style='float: right;' ng-if='currentPage!=1'><a href='Javascript:void(0)' ng-click='selectPage(currentPage-1)'>Newer</a></li>" +
    "  </ul>" +
    "</div>"
]