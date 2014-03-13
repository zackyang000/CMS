angular.module('article-detail-right-sidebar',['resource.articles','resource.comments'])

.controller('ArticleDetailRightSidebarCtrl',
["$scope","$routeParams","channel"
($scope,$routeParams,channel) ->
  #Categories list
  channel.get().then (channels) ->
    for item in channels
      if item.Name.toLowerCase()==$scope.item.Group.Channel.Name.toLowerCase()
        $scope.channel=item
        break
    $scope.group=$scope.item.Group
])