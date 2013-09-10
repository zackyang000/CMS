ArticleListController=["$scope","$routeParams","$location","Article", ($scope,$routeParams,$location,Article) ->
  $scope.$parent.showBanner=false
  $scope.$parent.title=$routeParams.group ? $routeParams.channel ? "Search Result '#{$scope.key}'"
  
  $scope.currentPage =$routeParams.p ? 1
  $scope.category = if $routeParams.type=='category' then $routeParams.query else ''
  $scope.tag = if $routeParams.type=='tag' then $routeParams.query else ''
  $scope.channel=$routeParams.channel
  $scope.group=$routeParams.group
  $scope.keyword=$routeParams.key

  $scope.setPage = (pageNo) ->
    $location.search({p: pageNo})

  $scope.load = ->
    $scope.loading=true
    filter='IsDeleted eq false'
    filter+=" and Group/Channel/Url eq '#{$routeParams.channel}'" if $routeParams.channel
    filter+=" and Group/Url eq '#{$routeParams.group}'" if $routeParams.group
    filter+=" and indexof(Title, '#{$routeParams.key}') gt -1" if $routeParams.key
    filter+=" and Categorys/any(category:category/Url eq '#{$scope.category}')" if $scope.category
    filter+=" and Tags/any(tag:tag/Name eq '#{$scope.tag}')" if $scope.tag
    Article.query
      $filter:filter
      $skip:($scope.currentPage-1)*10
    , (data)->
      scroll(0,0)
      $scope.list = data
      $scope.loading=false

  $scope.load $scope.currentPage
]