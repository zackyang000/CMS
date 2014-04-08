angular.module('admin-article-edit',['resource.articles','resource.channels'])

.factory("TranslateService", ["$http", ($http) ->
  events: (key) ->
    $http
      method: "JSONP"
      url: "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=JSON_CALLBACK&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + key
])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/article(':id')",
      templateUrl: "/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
    .when "/article/new",
      templateUrl: "/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
])


.controller('ArticleEditCtrl',
["$scope","$routeParams","$window","$rootScope","$fileUploader","Article","Channel","$timeout","TranslateService", "messager"
($scope,$routeParams,$window,$rootScope,$fileUploader,Article,Channel,$timeout,TranslateService, messager) ->
  $scope.channels=Channel.query $expand:'Groups',()->
    if $routeParams.id
      $scope.loading="Loading"
      Article.get 
        $filter:"PostId eq (guid'#{$routeParams.id}')"
        $expand:'Tags,Group/Channel'
      , (data)->
          $scope.entity=data.value[0]
          if $scope.entity.Group
            $scope.channelId=$scope.entity.Group.Channel.ChannelId
            $scope.groupId=$scope.entity.Group.GroupId
          if !$scope.entity.Url
            $scope.translateTitle()
          if $scope.entity.Tags
            $scope.tags=''
            for item in $scope.entity.Tags 
              $scope.tags+=','+item.Name
            $scope.tags=$scope.tags.substring(1)
          $scope.loading=""
    else
      $scope.entity={}

  $scope.getGroups = ->
    return undefined if $scope.channels.value is undefined
    for item in $scope.channels.value
      return item.Groups if item.ChannelId==$scope.channelId

  $scope.submit = ->
    $scope.isSubmit=true
    return if $scope.form.$invalid
    return if !$scope.channelValid()
    return if !$scope.groupValid()

    $scope.loading="Saving"

    if $scope.uploader.getNotUploadedItems().length
      $scope.uploader.uploadAll()
    else
      save()

  $scope.channelValid=->
    return $scope.channels.value

  $scope.groupValid=->
    return $scope.groupId

  save = ->
    entity=$scope.entity
    entity.Group={}
    entity.Group.GroupId=(item for item in $scope.getGroups() when item.GroupId is $scope.groupId)[0].GroupId
    entity.Tags=[]
    if $scope.tags
      for item in $scope.tags.split(",")
        entity.Tags.push({TagId:UUID.generate(),Name:item})
    if !$routeParams.id
      entity.PostId=UUID.generate()
      Article.save entity,
      (data) ->
        $window.location.href = "/post/#{data.Url}"
      ,(error) ->
        $scope.loading=""
    else
      Article.update {id:"(guid'#{entity.PostId}')"},entity,
      (data)->
        $window.location.href = "/post/#{data.Url}"
      ,(error) ->
        $scope.loading=""

  $scope.remove = ->
    messager.confirm ->
      $scope.loading="Deleting"
      entity=$scope.entity
      entity.IsDeleted=true
      Article.update {id:"(guid'#{entity.PostId}')"},entity
      ,(data)->
        messager.success "Delete post successfully."
        $window.location.href = "article"

  #上传图片
  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.apiHostTemp}/api/FileManage/upload"

  $scope.uploader.bind('success', (event, xhr, item, res) ->
    $scope.entity.Thumbnail = res.result
    save()
  )

  $scope.removeThumbnail = ()->
    $scope.entity.Thumbnail=undefined

  #URL根据Title翻译获取.
  timeout=undefined
  $scope.translateTitle = ->
    if $scope.entity.Title
      $timeout.cancel timeout if timeout
      timeout = $timeout(->
        $scope.Translating=true
        TranslateService.events($scope.entity.Title).success (data, status) ->
          data = $.trim(data)
          data = data.toLowerCase()
          data = data.replace(/[^_a-zA-Z\d\s]/g, '')
          data = data.replace(/[\s]/g, "-")
          $scope.entity.Url=data
          $scope.Translating=false
      , 500)
])