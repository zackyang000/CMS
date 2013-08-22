var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "$window", "$rootScope", "uploadManager", "Article", "Channel", function($scope, $routeParams, $window, $rootScope, uploadManager, Article, Channel) {
    var save;
    $scope.id = $routeParams.id;
    $scope.entity = {};
    $scope.entity.PostId = UUID.generate();
    $scope.thumbnail = {};
    $scope.channels = Channel.query({
      $expand: 'Groups,Groups/Categorys'
    });
    $scope.getGroups = function() {
      var item, _i, _len, _ref;
      if ($scope.channels.value === void 0) {
        return void 0;
      }
      _ref = $scope.channels.value;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.ChannelId === $scope.channelId) {
          return item.Groups;
        }
      }
    };
    $scope.getCategories = function() {
      var item, _i, _len, _ref;
      if ($scope.getGroups() === void 0) {
        return void 0;
      }
      _ref = $scope.getGroups();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.GroupId === $scope.groupId) {
          return item.Categorys;
        }
      }
    };
    $scope.submit = function() {
      $scope.isSubmit = true;
      if (!$scope.channelValid()) {
        return false;
      }
      if (!$scope.groupValid()) {
        return false;
      }
      if (!$scope.categoryValid()) {
        return false;
      }
      if (!$scope.entity.Url) {
        return false;
      }
      if (!$scope.entity.Title) {
        return false;
      }
      if (!$scope.entity.Content) {
        return false;
      }
      if (!$scope.entity.Description) {
        return false;
      }
      if ($scope.files.length) {
        return uploadManager.upload();
      } else {
        return save();
      }
    };
    $scope.channelValid = function() {
      if ($scope.getGroups()) {
        return true;
      }
      return false;
    };
    $scope.groupValid = function() {
      if ($scope.getCategories()) {
        return true;
      }
      return false;
    };
    $scope.categoryValid = function() {
      var item, _i, _len, _ref;
      if (!$scope.getCategories()) {
        return false;
      }
      _ref = $scope.getCategories();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.checked) {
          return true;
        }
      }
      return false;
    };
    save = function() {
      var entity, item, _i, _j, _len, _len1, _ref, _ref1;
      entity = $scope.entity;
      entity.PostId = UUID.generate();
      entity.Group = {};
      entity.Group.GroupId = ((function() {
        var _i, _len, _ref, _results;
        _ref = $scope.getGroups();
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          if (item.GroupId === $scope.groupId) {
            _results.push(item);
          }
        }
        return _results;
      })())[0].GroupId;
      entity.Categorys = [];
      _ref = $scope.getCategories();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.checked) {
          entity.Categorys.push({
            CategoryId: item.CategoryId
          });
        }
      }
      entity.Tags = [];
      if ($scope.tags) {
        _ref1 = $scope.tags.split(",");
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          item = _ref1[_j];
          entity.Tags.push({
            TagId: UUID.generate(),
            Name: item
          });
        }
      }
      if ($scope.source) {
        entity.Source = $scope.source;
        entity.Source.SourceId = UUID.generate();
      }
      return Article.save(entity, function(data) {
        return $window.location.href = "/#!/post/" + entity.Url;
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
    $rootScope.$on("fileAdded", function(e, call) {
      $scope.files.push(call);
      return $scope.$apply();
    });
    return $rootScope.$on("fileUploaded", function(e, call) {
      $scope.entity.Thumbnail = {
        ThumbnailId: UUID.generate(),
        Title: $scope.entity.Title,
        Url: call.result
      };
      return save();
    });
  }
];
