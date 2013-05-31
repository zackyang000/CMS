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

  $scope.turnpages=(page)->
    $scope.$parent.loading=true
    $scope.page=page
    $scope.list = Article.querybypaged
      page:$scope.page
      channel:$scope.channel
      group:$scope.group
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.search
    , ->
      $scope.$parent.loading=false
    #TODO:此处需要改变URL但不触发$locationProvider事件.
    #$location.path('/list/technologies/codes/2')
    #$location.replace()
  $scope.turnpages $scope.page 
]