angular.module('admin-gallery-edit',['resource.galleries','resource.photos'])

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
["$scope","$routeParams","$location","$rootScope","$fileUploader","Gallery","Photo"
($scope,$routeParams,$location,$rootScope,$fileUploader,Gallery,Photo) ->
  $scope.get = ->
    if $routeParams.id
      Gallery.get 
        $filter:"GalleryId eq (guid'#{$routeParams.id}')"
        $expand:"Photos"
      ,(data)->
        $scope.entity=data.value[0]
        $scope.options =
          url: "#{config.baseAddress}/api/FileManage/Photo/#{$routeParams.id}"
          maxFilesize: 100
          addRemoveLinks: false
          acceptedFiles: "image/*"
        galleryInit()
    else
      $scope.entity = {}

  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.baseAddress}/api/FileManage/upload"

  $scope.submit = ->
    $scope.isSubmit=true

    return false if !$scope.entity.Name

    if $scope.uploader.getNotUploadedItems().length
      $scope.uploader.uploadAll()
    else
      save()

  save = ->
    entity=$scope.entity
    if !$routeParams.id
      entity.GalleryId=UUID.generate()
    Gallery.update {id:"(guid'#{entity.GalleryId}')"},entity,(data)->
      message.success "Save category successfully."
      if entity.CreateDate
        $scope.get()
      else
        $location.path("gallery('#{entity.GalleryId}')")

  $scope.removeImg = ->
    $scope.entity.Cover = undefined

  #上传封面处理
  $scope.uploader.bind('success', (event, xhr, item, res) ->
    $scope.entity.Cover = res.result
    save()
  )

  #上传照片处理


  $scope.removePhoto = (item)->
    message.confirm ->
      $scope.loading="Delete"
      item.IsDeleted=true
      Photo.update {id:"(guid'#{item.PhotoId}')"},item,(data)->
        message.success "Delete successfully."
        $scope.loading=""
        $scope.get()

  $scope.get()




])