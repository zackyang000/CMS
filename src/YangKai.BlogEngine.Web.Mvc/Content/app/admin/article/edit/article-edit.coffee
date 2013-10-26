angular.module('admin-article-edit',[])

.config(["$routeProvider",
($routeProvider) ->
  $routeProvider
  .when("/article(':id')",
    templateUrl: "/content/app/admin/article/edit/article-edit.tpl.html"
    controller: 'ArticleEditCtrl')
  .when("/article/new",
    templateUrl: "/content/app/admin/article/edit/article-edit.tpl.html"
    controller: 'ArticleEditCtrl')
])

.controller('ArticleEditCtrl',
["$scope","$routeParams","$window","$rootScope","uploadManager","Article","Channel",
($scope,$routeParams,$window,$rootScope,uploadManager,Article,Channel) ->
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
    #valid
    $scope.isSubmit=true
    return false if !$scope.channelValid()
    return false if !$scope.groupValid()
    return false if !$scope.categoryValid()
    return false if !$scope.entity.Url
    return false if !$scope.entity.Title
    return false if !$scope.entity.Content

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
    if !$routeParams.id
      entity.PostId=UUID.generate()
      Article.save entity,(data)->
        $window.location.href = "/#!/post/#{data.Url}"
    else
      Article.update {id:"(guid'#{entity.PostId}')"},entity,(data)->
        $window.location.href = "/#!/post/#{data.Url}"

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

  #翻译Title获取URL
  $scope.getUrl = ->
    $scope.TranslateUrl=true
    $window.mycallback = (response) ->
      response = $.trim(response)
      response = response.toLowerCase()
      response = response.replace(/[^_a-zA-Z\d\s]/g, '')
      response = response.replace(/[\s]/g, "-")
      $scope.entity.Url=response
      $scope.TranslateUrl=false
      $scope.$apply()
    s = document.createElement("script")
    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + $scope.entity.Title
    document.getElementsByTagName("head")[0].appendChild(s)

])