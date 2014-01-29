angular.module('article-list-right-sidebar',['resource.articles','resource.comments'])

.controller('ArticleListRightSidebarCtrl',
["$scope","$routeParams"
($scope,$routeParams) ->
  #Categories list
  for item in $scope.Channels
    if item.Name.toLowerCase()==$routeParams.channel.toLowerCase()
      $scope.channel=item
      break
  if $routeParams.group
    for item in $scope.channel.Groups
      if item.Name.toLowerCase()==$routeParams.group.toLowerCase()
        $scope.group=item
        break
])