ArticleListController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.$parent.showBanner=false

  $scope.currentPage =$routeParams.page ? 1
  $scope.category = if $routeParams.type=='category' then $routeParams.query else ''
  $scope.tag = if $routeParams.type=='tag' then $routeParams.query else ''
  $scope.date = if $routeParams.type=='date' then $routeParams.query else ''
  $scope.key = $routeParams.key ? ''

  $scope.$parent.title=$routeParams.group ? $routeParams.channel

  $scope.expand=(item)->
    item.isShowDetail = not item.isShowDetail
    codeformat()



  $scope.setPage = (pageNo) ->
    $scope.loading=true
    filter='1 eq 1'
    filter="Group/Channel/Url eq '#{$routeParams.channel}'" if $routeParams.channel
    filter="Group/Url eq '#{$routeParams.group}'" if $routeParams.group
    Article.query
      $filter:filter
      $skip:(pageNo-1)*10
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.key
    , (data)->
      scroll(0,0)
      $scope.list = data
      $scope.loading=false

  $scope.setPage 1
]