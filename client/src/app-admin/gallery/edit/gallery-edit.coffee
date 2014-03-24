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
["$scope","$routeParams","$location","$rootScope","uploadManager","Gallery","Photo"
($scope,$routeParams,$location,$rootScope,uploadManager,Gallery,Photo) ->
 $scope.imgHost = config.imgHost
 $scope.uploadUrl = "#{config.baseAddress}/FileManage"

 $scope.get = ->
    if $routeParams.id
      Gallery.get 
        $filter:"GalleryId eq (guid'#{$routeParams.id}')"
        $expand:"Photos"
      ,(data)->
        $scope.entity=data.value[0]
        uploadInit("/FileManage/Photo/#{$scope.entity.GalleryId}")
        galleryInit()
    else
      $scope.entity = {}

  $scope.submit = ->
    #valid
    $scope.isSubmit=true
    return false if !$scope.entity.Name

    if $scope.files.length
      uploadManager.upload()
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
        $scope.files = []
      else
        $location.path("gallery('#{entity.GalleryId}')")

  #上传封面处理
  $scope.files = []

  $scope.removeImg = (file)->
    deleteFile=f for f in $scope.files when f.name is file.name
    $scope.files.splice($scope.files.indexOf(deleteFile),1)
    uploadManager.cancel file
  
  $scope.removeServerImg = ()->
    $scope.entity.Cover=undefined

  $rootScope.$on "fileAdded", (e, call) ->
    $scope.files.push call
    $scope.$apply()

  $rootScope.$on "fileUploaded", (e, call) ->
    if !$scope.entity.Cover
      debugger
      $scope.entity.Cover=JSON.parse(call.result).result
      save()

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