angular.module('gallery-edit', ['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/new",
      templateUrl: "/app-admin/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
    .when "/gallery/:id",
      templateUrl: "/app-admin/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
])

.controller('GalleryEditCtrl',
["$scope","$routeParams","$location","$rootScope","$fileUploader","Galleries", "messager"
($scope,$routeParams,$location,$rootScope,$fileUploader,Galleries, messager) ->
  $scope.get = ->
    if $routeParams.id
      Galleries.get
        id: $routeParams.id
      ,(data)->
        $scope.entity = data
        $scope.options =
          url: "#{config.apiHost}/file-upload/?path=gallery/#{$routeParams.id}"
          maxFilesize: 100
          addRemoveLinks: false
          acceptedFiles: "image/*"
          success: (req, res) ->
            debugger
            $scope.entity.photos.push res

        galleryInit()
    else
      $scope.entity = {}



  $scope.submit = ->
    $scope.isSubmit=true
    return false if !$scope.entity.name
    $scope.loading="Saving"
    if $scope.uploader.getNotUploadedItems().length
      $scope.uploader.uploadAll()
    else
      save()

  save = ->
    $scope.loading="Saving"
    entity = $scope.entity
    if !$routeParams.id
      Galleries.save entity, (data)->
        messager.success "Save successfully."
        $location.path("gallery/#{entity._id}")
    else
      Galleries.update {id:"#{entity._id}"},entity,(data)->
        messager.success "Save successfully."


  #上传封面
  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.apiHost}/file-upload"

  $scope.uploader.bind('success', (event, xhr, item, res) ->
    $scope.entity.cover = res.result
    save()
  )

  $scope.removeCover = ->
    $scope.entity.cover = null

  #上传照片
  $scope.removePhoto = (item)->
    $scope.entity.photos.splice($scope.entity.photos.indexOf(item), 1)


  $scope.get()
])