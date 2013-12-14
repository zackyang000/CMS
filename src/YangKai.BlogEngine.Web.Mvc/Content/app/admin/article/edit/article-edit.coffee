angular.module('admin-article-edit',[])

.factory("TranslateService", ["$http", ($http) ->
  events: (key) ->
    $http
      method: "JSONP"
      url: "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=JSON_CALLBACK&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + key
])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/admin/article(':id')",
    templateUrl: "/content/app/admin/article/edit/article-edit.tpl.html"
    controller: 'ArticleEditCtrl')
  .when("/admin/article/new",
    templateUrl: "/content/app/admin/article/edit/article-edit.tpl.html"
    controller: 'ArticleEditCtrl')
])


.controller('ArticleEditCtrl',
["$scope","$routeParams","$window","$rootScope","uploadManager","Article","Channel","$timeout","TranslateService"
($scope,$routeParams,$window,$rootScope,uploadManager,Article,Channel,$timeout,TranslateService) ->
  $scope.channels=Channel.query $expand:'Groups,Groups/Categorys',()->
    if $routeParams.id
      $scope.loading="Loading"
      Article.get $filter:"PostId eq (guid'#{$routeParams.id}')",(data)->
        $scope.entity=data.value[0]
        $scope.channelId=$scope.entity.Group.Channel.ChannelId
        $scope.groupId=$scope.entity.Group.GroupId
        #加载Category
        for category in $scope.entity.Categorys
          for item in $scope.getCategories() when item.CategoryId is category.CategoryId
            item.checked=true
        #加载Tag
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

  $scope.getCategories = ->
    return undefined if $scope.getGroups() is undefined
    for item in $scope.getGroups()
      return item.Categorys if item.GroupId==$scope.groupId

  $scope.categorySelect = (item) ->
    item.checked=if item.checked then true else false

  $scope.submit = ->
    $scope.isSubmit=true
    return if $scope.form.$invalid
    return if !$scope.channelValid()
    return if !$scope.groupValid()
    return if !$scope.categoryValid()

    $scope.loading="Saving"

    if $scope.files.length
      uploadManager.upload()
    else
      save()

  $scope.channelValid=->
    return true if $scope.getGroups()
    return false

  $scope.groupValid=->
    return true if $scope.getCategories()
    return false

  $scope.categoryValid=->
    return false if !$scope.getCategories()
    for item in $scope.getCategories() when item.checked
      return true
    return false

  save = ->
    entity=$scope.entity
    entity.Group={}
    entity.Group.GroupId=(item for item in $scope.getGroups() when item.GroupId is $scope.groupId)[0].GroupId
    entity.Categorys=[]
    for item in $scope.getCategories() when item.checked
      entity.Categorys.push({CategoryId:item.CategoryId})
    entity.Tags=[]
    if $scope.tags
      for item in $scope.tags.split(",")
        entity.Tags.push({TagId:UUID.generate(),Name:item})
    if entity.Source
      entity.Source.SourceId=UUID.generate()
      entity.Source.Title=entity.Title
    if !$routeParams.id
      entity.PostId=UUID.generate()
      Article.save entity,(data)->
        $window.location.href = "/post/#{data.Url}"
    else
      Article.update {id:"(guid'#{entity.PostId}')"},entity,(data)->
        $window.location.href = "/post/#{data.Url}"

  $scope.remove = ->
    message.confirm ->
      $scope.loading="Deleting"
      entity=$scope.entity
      entity.IsDeleted=true
      debugger
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
    $scope.entity.Thumbnail=
      ThumbnailId:UUID.generate()
      Title:$scope.entity.Title
      Url:call.result
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