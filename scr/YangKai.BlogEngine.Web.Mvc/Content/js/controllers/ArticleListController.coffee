ArticleListController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.$parent.showBanner=false

  $scope.page =$routeParams.page ? 1
  $scope.channel = $routeParams.channel ? ''
  $scope.group =$routeParams.group ? ''
  $scope.category = if $routeParams.type=='category' then $routeParams.query else ''
  $scope.tag = if $routeParams.type=='tag' then $routeParams.query else ''
  $scope.date = if $routeParams.type=='date' then $routeParams.query else ''
  $scope.key = $routeParams.key ? ''

  $scope.$parent.title=$routeParams.group ? $routeParams.channel

  $scope.expand=(item)->
    item.isShowDetail = not item.isShowDetail
    codeformat()

  $scope.turnpages=(page)->
    $scope.loading=true
    $scope.page=page
    result = Article.querybypaged
      page:$scope.page
      channel:$scope.channel
      group:$scope.group
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.key
    , ->
      if page==1
        $scope.list = result
      else
        $scope.list.DataList = $scope.list.DataList.concat(result.DataList)
      $scope.loading=false

  $scope.turnpages $scope.page 
]