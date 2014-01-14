
angular.module('admin-gallery-edit', ['resource.galleries', 'resource.photos']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/admin/gallery(':id')", {
      templateUrl: "/content/app/admin/gallery/edit/gallery-edit.tpl.html",
      controller: 'GalleryEditCtrl'
    }).when("/admin/gallery/new", {
      templateUrl: "/content/app/admin/gallery/edit/gallery-edit.tpl.html",
      controller: 'GalleryEditCtrl'
    });
  }
]).controller('GalleryEditCtrl', [
  "$scope", "$routeParams", "$location", "$rootScope", "uploadManager", "Gallery", "Photo", function($scope, $routeParams, $location, $rootScope, uploadManager, Gallery, Photo) {
    var save;
    $scope.get = function() {
      if ($routeParams.id) {
        return Gallery.get({
          $filter: "GalleryId eq (guid'" + $routeParams.id + "')",
          $expand: "Photos"
        }, function(data) {
          $scope.entity = data.value[0];
          uploadInit("/FileManage/Photo/" + $scope.entity.GalleryId);
          return galleryInit();
        });
      } else {
        return $scope.entity = {};
      }
    };
    $scope.submit = function() {
      $scope.isSubmit = true;
      if (!$scope.entity.Name) {
        return false;
      }
      if ($scope.files.length) {
        return uploadManager.upload();
      } else {
        return save();
      }
    };
    save = function() {
      var entity;
      entity = $scope.entity;
      if (!$routeParams.id) {
        entity.GalleryId = UUID.generate();
      }
      return Gallery.update({
        id: "(guid'" + entity.GalleryId + "')"
      }, entity, function(data) {
        message.success("Save category successfully.");
        if (entity.CreateDate) {
          $scope.get();
          return $scope.files = [];
        } else {
          return $location.path("/admin/gallery('" + entity.GalleryId + "')");
        }
      });
    };
    $scope.files = [];
    $scope.removeImg = function(file) {
      var deleteFile, f, _i, _len, _ref;
      _ref = $scope.files;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        if (f.name === file.name) {
          deleteFile = f;
        }
      }
      $scope.files.splice($scope.files.indexOf(deleteFile), 1);
      return uploadManager.cancel(file);
    };
    $scope.removeServerImg = function() {
      return $scope.entity.Cover = void 0;
    };
    $rootScope.$on("fileAdded", function(e, call) {
      $scope.files.push(call);
      return $scope.$apply();
    });
    $rootScope.$on("fileUploaded", function(e, call) {
      if (!$scope.entity.Cover) {
        $scope.entity.Cover = call.result;
        return save();
      }
    });
    $scope.removePhoto = function(item) {
      return message.confirm(function() {
        $scope.loading = "Delete";
        item.IsDeleted = true;
        return Photo.update({
          id: "(guid'" + item.PhotoId + "')"
        }, item, function(data) {
          message.success("Delete successfully.");
          $scope.loading = "";
          return $scope.get();
        });
      });
    };
    return $scope.get();
  }
]);
