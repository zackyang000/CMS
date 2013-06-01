ArticleListController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.$parent.showBanner=false

  $scope.page =$routeParams.page ? 1
  $scope.channel = $routeParams.channel ? ''
  $scope.group =$routeParams.group ? ''
  $scope.category = if $routeParams.type=='category' then $routeParams.query else ''
  $scope.tag = if $routeParams.type=='tag' then $routeParams.query else ''
  $scope.date = if $routeParams.type=='date' then $routeParams.query else ''
  $scope.search = if $routeParams.type=='search' then $routeParams.query else ''

  $scope.expand=(item)->
    item.isShowDetail = not item.isShowDetail
    codeformat()

  $scope.turnpages=(page)->
    $scope.$parent.loading=true
    $scope.page=page
    result = Article.querybypaged
      page:$scope.page
      channel:$scope.channel
      group:$scope.group
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.search
    , ->
      if page==1
        $scope.list = result
      else
        $scope.list.DataList = $scope.list.DataList.concat(result.DataList).slice(-500)
      $scope.$parent.loading=false

  $scope.turnpages $scope.page 
]