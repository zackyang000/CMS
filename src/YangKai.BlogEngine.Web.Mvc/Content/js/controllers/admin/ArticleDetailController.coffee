ArticleDetailController=["$scope","$routeParams","$window","Article","Channel",
($scope,$routeParams,$window,Article,Channel) ->
  $scope.id =$routeParams.id

  $scope.entity={}
  $scope.thumbnail={}

  $scope.channels=Channel.query $expand:'Groups,Groups/Categorys'

  $scope.getGroups = ->
    return undefined if $scope.channels.value is undefined
    for item in $scope.channels.value
      return item.Groups if item.ChannelId==$scope.channelId

  $scope.getCategories = ->
    return undefined if $scope.getGroups() is undefined
    for item in $scope.getGroups()
      return item.Categorys if item.GroupId==$scope.groupId

  $scope.submit = ->
    #valid


    #save
    entity=$scope.entity
    entity.PostId=UUID.generate()
    entity.Group={}
    entity.Group.GroupId=(item for item in $scope.getGroups() when item.GroupId is $scope.groupId)[0].GroupId
    entity.Categorys=[]
    for item in $scope.getCategories() when item.checked
      entity.Categorys.push({CategoryId:item.CategoryId})
    entity.Tags=[]
    if $scope.tags
      for item in $scope.tags.split(",")
        entity.Tags.push({TagId:UUID.generate(),Name:item})
    if $scope.source
      entity.Source=$scope.source 
      entity.Source.SourceId=UUID.generate()
    entity.Thumbnail=null
    Article.save entity,(data)->
      $window.location.href = "/#!/post/#{entity.Url}"
]