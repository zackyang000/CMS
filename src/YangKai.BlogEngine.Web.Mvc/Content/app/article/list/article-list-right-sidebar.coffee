angular.module('article-list-right-sidebar',['resource.articles','resource.comments','ChannelServices'])

.controller('ArticleListRightSidebarCtrl',
["$scope","$routeParams","channel"
($scope,$routeParams,channel) ->
  #Categories list
  if $routeParams.channel
    channel.get().then (channels) ->
      for item in channels
        if item.Name.toLowerCase()==$routeParams.channel.toLowerCase()
          $scope.channel=item
          break
      setGroup()
  else
    channel.getdefault().then (channel) ->
      $scope.channel=channel
      setGroup()

  setGroup = ->
    if $routeParams.group
      for item in $scope.channel.Groups
        if item.Name.toLowerCase()==$routeParams.group.toLowerCase()
          $scope.group=item
          break
])