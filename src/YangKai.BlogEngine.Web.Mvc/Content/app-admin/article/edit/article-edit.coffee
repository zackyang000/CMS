angular.module('admin-article-edit',['resource.articles','resource.channels'])

.factory("TranslateService", ["$http", ($http) ->
  events: (key) ->
    $http
      method: "JSONP"
      url: "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=JSON_CALLBACK&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + key
])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/admin/article(':id')",
      templateUrl: "/content/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
    .when "/admin/article/new",
      templateUrl: "/content/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
])


.controller('ArticleEditCtrl',
["$scope","$routeParams","$window","$rootScope","uploadManager","Article","Channel","$timeout","TranslateService"
($scope,$routeParams,$window,$rootScope,uploadManager,Article,Channel,$timeout,TranslateService) ->
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

    if $scope.files.length
      uploadManager.upload()
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
    message.confirm ->
      $scope.loading="Deleting"
      entity=$scope.entity
      entity.IsDeleted=true
      Article.update {id:"(guid'#{entity.PostId}')"},entity
      ,(data)->
        message.success "Delete post successfully."
        $window.location.href = "/admin/article"

  #上传图片处理
  $scope.files = []

  $scope.removeImg = (file)->
    deleteFile=f for f in $scope.files when f.name is file.name
    $scope.files.splice($scope.files.indexOf(deleteFile),1)
    uploadManager.cancel file
  
  $scope.removeServerImg = ()->
    $scope.entity.Thumbnail=null

  $rootScope.$on "fileAdded", (e, call) ->
    $scope.files.push call
    $scope.$apply()

  $rootScope.$on "fileUploaded", (e, call) ->
    $scope.entity.Thumbnail=call.result
    save()

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