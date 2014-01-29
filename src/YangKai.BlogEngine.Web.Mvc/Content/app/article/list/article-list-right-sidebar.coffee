angular.module('article-list-right-sidebar',['resource.articles','resource.comments','ChannelServices'])

.controller('ArticleListRightSidebarCtrl',
["$scope","$routeParams","channel"
($scope,$routeParams,channel) ->
  #Categories list
  channel.get().then (channels) ->
    if $routeParams.channel
      for item in channels
        if item.Name.toLowerCase()==$routeParams.channel.toLowerCase()
          $scope.channel=item
          break
    else
      debugger 
      #todo 判断default
      #todo 判断default
      #todo 判断default
      #todo 判断default
      #todo 判断default
      $scope.channel=channels[0]

  if $routeParams.group
    for item in $scope.channel.Groups
      if item.Name.toLowerCase()==$routeParams.group.toLowerCase()
        $scope.group=item
        break
])