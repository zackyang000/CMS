var factory;

angular.module("customDirectives", []).directive("upload", [
  "uploadManager", factory = function(uploadManager) {
    return {
      restrict: "A",
      link: function(scope, element, attrs) {
        return $(element).fileupload({
          dataType: "text",
          add: function(e, data) {
            return uploadManager.add(data);
          },
          progressall: function(e, data) {
            var progress;
            progress = parseInt(data.loaded / data.total * 100, 10);
            return uploadManager.setProgress(progress);
          },
          done: function(e, data) {
            return uploadManager.setFileStatus(data);
          }
        });
      }
    };
  }
]);

angular.module("customDirectives", []).directive("dynamicUpload", [
  '$compile', function($compile) {
    return function(scope, element, attrs) {
      var update;
      update = function(value) {
        element.context.innerHTML = "<input type='file' name='file' data-url='fileupload/upload/" + value + "' upload />";
        return $compile(element.contents())(scope);
      };
      return scope.$watch(attrs.dynamicUpload, function(value) {
        return update('1');
      });
    };
  }
]);

angular.module("FileUpload", []).factory("uploadManager", [
  "$rootScope", function($rootScope) {
    var _files;
    _files = [];
    return {
      add: function(file) {
        _files.push(file);
        return $rootScope.$broadcast("fileAdded", file.files[0]);
      },
      cancel: function(file) {
        var deleteFile, f, _i, _len;
        for (_i = 0, _len = _files.length; _i < _len; _i++) {
          f = _files[_i];
          if (f.files[0].name === file.name) deleteFile = f;
        }
        return _files.splice(_files.indexOf(deleteFile), 1);
      },
      clear: function() {
        return _files = [];
      },
      files: function() {
        var fileNames;
        fileNames = [];
        $.each(_files, function(index, file) {
          return fileNames.push(file.files[0].name);
        });
        return fileNames;
      },
      upload: function() {
        $.each(_files, function(index, file) {
          return file.submit();
        });
        return this.clear();
      },
      setProgress: function(percentage) {
        return $rootScope.$broadcast("uploadProgress", percentage);
      },
      setFileStatus: function(data) {
        return $rootScope.$broadcast("fileUploaded", data);
      }
    };
  }
]);
