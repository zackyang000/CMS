angular.module('article-edit',['resource.articles','resource.categories'])

.factory("TranslateService", ["$http", ($http) ->
  translate: (key) ->
    $http
      method: "JSONP"
      url: "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=JSON_CALLBACK&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + key
])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/article/new",
      templateUrl: "/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
    .when "/article/:id",
      templateUrl: "/app-admin/article/edit/article-edit.tpl.html"
      controller: 'ArticleEditCtrl'
])

.controller('ArticleEditCtrl',
["$scope","$routeParams","$window","$rootScope","$fileUploader","Articles","Categories","$timeout","TranslateService", "messager"
($scope,$routeParams,$window,$rootScope,$fileUploader,Articles,Categories,$timeout,TranslateService, messager) ->
  Categories.query (categories) ->
    $scope.categories = categories.value
    if $routeParams.id
      $scope.loading = "Loading"
      Articles.get
        id : $routeParams.id
      , (data)->
          $scope.entity = data
          if !$scope.entity.url
            $scope.translateTitle()
          if $scope.entity.meta.tags
            $scope.tags = $scope.entity.meta.tags.join(',')
          $scope.loading = ""
    else
      $scope.entity =
        meta : {}
        date: new Date()
        comments : []

  $scope.submit = ->
    $scope.isSubmit = true
    return if $scope.form.$invalid
    return if !$scope.entity.category

    $scope.loading = "Saving"

    if $scope.uploader.getNotUploadedItems().length
      $scope.uploader.uploadAll()
    else
      save()

  save = ->
    entity = $scope.entity
    if $scope.tags
      $scope.entity.meta.tags = $scope.tags.split(',')

    if !$routeParams.id
      Articles.save entity
      ,(data) ->
        $window.location.href = "/post/#{data.url}"
      ,(error) ->
        $scope.loading=""
    else
      Articles.update {id: $routeParams.id}, entity
      ,(data)->
        $window.location.href = "/post/#{data.url}"
      ,(error) ->
        $scope.loading=""

  $scope.remove = ->
    messager.confirm ->
      $scope.loading="Deleting"
      entity=$scope.entity
      Articles.delete { id: entity._id }, (data)->
        messager.success "Delete post successfully."
        $window.location.href = "article"

  #上传图片
  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.apiHost}/api/FileManage/upload"

  $scope.uploader.bind('success', (event, xhr, item, res) ->
    $scope.entity.Thumbnail = res.result
    save()
  )

  $scope.removeThumbnail = ()->
    $scope.entity.Thumbnail=undefined

  #根据title翻译url.
  timeout = undefined
  $scope.translateTitle = ->
    if $scope.entity.title
      $timeout.cancel timeout if timeout
      timeout = $timeout( ->
        $scope.translating = true
        TranslateService.translate($scope.entity.title).success (data) ->
          data = $.trim(data)
          data = data.toLowerCase()
          data = data.replace(/[^_a-zA-Z\d\s]/g, '')
          data = data.replace(/[\s]/g, "-")
          $scope.entity.url = data
          $scope.translating = false
      , 500)
])