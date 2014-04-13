angular.module('gallery-edit',['resource.galleries','resource.photos'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery(':id')",
      templateUrl: "/app-admin/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
    .when "/gallery/new",
      templateUrl: "/app-admin/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
])

.controller('GalleryEditCtrl',
["$scope","$routeParams","$location","$rootScope","$fileUploader","Gallery","Photo", "messager"
($scope,$routeParams,$location,$rootScope,$fileUploader,Gallery,Photo, messager) ->
  $scope.get = ->
    if $routeParams.id
      Gallery.get 
        $filter:"GalleryId eq (guid'#{$routeParams.id}')"
        $expand:"Photos"
      ,(data)->
        $scope.entity=data.value[0]
        $scope.options =
          url: "#{config.apiHost}/api/FileManage/Photo/#{$routeParams.id}"
          maxFilesize: 100
          addRemoveLinks: false
          acceptedFiles: "image/*"
        galleryInit()
    else
      $scope.entity = {}



  $scope.submit = ->
    $scope.isSubmit=true

    return false if !$scope.entity.Name
    $scope.loading="Saving"
    if $scope.uploader.getNotUploadedItems().length
      $scope.uploader.uploadAll()
    else
      save()

  save = ->
    entity=$scope.entity
    if !$routeParams.id
      entity.GalleryId=UUID.generate()
    Gallery.update {id:"(guid'#{entity.GalleryId}')"},entity,(data)->
      messager.success "Save category successfully."
      if entity.CreateDate
        $scope.get()
      else
        $location.path("gallery('#{entity.GalleryId}')")
      $scope.loading=""


  #上传封面
  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.apiHost}/api/FileManage/upload"

  $scope.uploader.bind('success', (event, xhr, item, res) ->
    $scope.entity.Cover = res.result
    save()
  )

  $scope.removeCover = ->
    $scope.entity.Cover = undefined

  #上传照片
  $scope.removePhoto = (item)->
    messager.confirm ->
      $scope.loading="Delete"
      item.IsDeleted=true
      Photo.update {id:"(guid'#{item.PhotoId}')"},item,(data)->
        messager.success "Delete successfully."
        $scope.loading=""
        $scope.get()

  $scope.get()
])