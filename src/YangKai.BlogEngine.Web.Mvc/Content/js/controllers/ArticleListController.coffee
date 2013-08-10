ArticleListController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.$parent.showBanner=false

  $scope.currentPage =$routeParams.p ? 1
  $scope.category = if $routeParams.type=='category' then $routeParams.query else ''
  $scope.tag = if $routeParams.type=='tag' then $routeParams.query else ''
  $scope.date = if $routeParams.type=='date' then $routeParams.query else ''
  $scope.key = $routeParams.key ? ''

  $scope.$parent.title=$routeParams.group ? $routeParams.channel ? "Search Result '#{$scope.key}'"

  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.load = ->
    $scope.loading=true
    filter='1 eq 1'
    filter="Group/Channel/Url eq '#{$routeParams.channel}'" if $routeParams.channel
    filter="Group/Url eq '#{$routeParams.group}'" if $routeParams.group
    filter="indexof(Title, '#{$routeParams.key}') gt 0" if $routeParams.key
    Article.query
      $filter:filter
      $skip:($scope.currentPage-1)*10
      category:$scope.category
      tag:$scope.tag
      date:$scope.date
      search:$scope.key
    , (data)->
      scroll(0,0)
      $scope.list = data
      $scope.loading=false

  $scope.load $scope.currentPage
]