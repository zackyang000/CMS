angular.module('gallery-edit', ['resource.galleries'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/gallery/new",
      templateUrl: "/app/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
      resolve:
        gallery: -> {}
    .when "/gallery/:id",
      templateUrl: "/app/gallery/edit/gallery-edit.tpl.html"
      controller: 'GalleryEditCtrl'
      resolve:
        gallery: ["$q", "$route", "Galleries", ($q, $route, Galleries)->
          deferred = $q.defer()
          Galleries.get
            $filter: "_id eq '#{$route.current.params.id}'"
          ,(data) ->
            deferred.resolve data.value[0]
          deferred.promise
        ]
])

.controller('GalleryEditCtrl',
["$scope", "$routeParams", "$location", "$rootScope", "$fileUploader", "Galleries", "gallery", "messager"
($scope, $routeParams, $location, $rootScope, $fileUploader, Galleries, gallery, messager) ->
  $scope.languages = config.languages
  $scope.get = ->
    $scope.entity = gallery || {}
    $scope.options =
      url: "#{config.url.api}/file-upload/?path=gallery/#{$routeParams.id}/photo&resize=1000x1000&thumbnail=973x615"
      maxFilesize: 100
      addRemoveLinks: false
      acceptedFiles: "image/*"
      success: (req, res) ->
        $scope.entity.photos.push
          name: req.name.substr(0, req.name.lastIndexOf('.')) || req.name
          description: ""
          thumbnail: res.replace(res.split('.').pop(),'thumbnail.' + res.split('.').pop())
          url: res
    galleryInit()

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
    entity.date = new Date()
    if !$routeParams.id
      Galleries.save entity, (data)->
        messager.success "Save successfully."
        $location.path("gallery/#{data._id}")
    else
      Galleries.update {id:"#{entity._id}"},entity,(data)->
        messager.success "Save successfully."

  $scope.changeUploadStatus = ->
    save()  if $scope.uploadPhoto
    $scope.uploadPhoto = !$scope.uploadPhoto

  #上传封面
  $scope.uploader = $fileUploader.create
    scope: $scope
    url: "#{config.url.api}/file-upload/?name=cover&path=gallery/#{$routeParams.id}&resize=973x615&thumbnail=973x615"

  $scope.uploader.bind('success', (event, xhr, item, res) ->
    #使用等比例的缩略图作为封面
    $scope.entity.cover = res.replace(res.split('.').pop(),'thumbnail.' + res.split('.').pop())
    save()
  )

  $scope.removeCover = ->
    $scope.entity.cover = null

  #上传照片
  $scope.removePhoto = (item)->
    $scope.entity.photos.splice($scope.entity.photos.indexOf(item), 1)


  $scope.get()
])
