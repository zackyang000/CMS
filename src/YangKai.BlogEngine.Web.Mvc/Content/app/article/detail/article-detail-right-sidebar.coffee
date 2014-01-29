angular.module('article-detail-right-sidebar',['resource.articles','resource.comments'])

.controller('ArticleDetailRightSidebarCtrl',
["$scope","$routeParams"
($scope,$routeParams) ->
  #Categories list
  for item in $scope.Channels
    if item.Name.toLowerCase()==$scope.item.Group.Channel.Name.toLowerCase()
      $scope.channel=item
      break
  $scope.group=$scope.item.Group
])