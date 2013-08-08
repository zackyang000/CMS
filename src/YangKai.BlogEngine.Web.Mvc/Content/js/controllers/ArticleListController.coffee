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
    $scope.currentPage=pageNo
    filter="Group/Channel/Url eq '#{$routeParams.channel}'" if $routeParams.channel
    filter="Group/Url eq '#{$routeParams.group}'" if $routeParams.group
    Article.query
      $filter:filter
      $skip:($scope.currentPage-1)*10
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.key
    , (data)->
      $scope.list = data.value
      $scope.pager={count:data['odata.count'],nextLink:data['nextLink']}
      $scope.numPages=Math.ceil($scope.pager.count / 10)
      scroll(0,0)
      $scope.loading=false

  $scope.setPage $scope.currentPage 
]