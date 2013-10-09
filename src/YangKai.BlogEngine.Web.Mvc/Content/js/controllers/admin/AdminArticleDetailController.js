var AdminArticleDetailController;

AdminArticleDetailController = [
  "$scope", "$routeParams", "$window", "$rootScope", "uploadManager", "Article", "Channel", function($scope, $routeParams, $window, $rootScope, uploadManager, Article, Channel) {
    var isNew, save;
    isNew = false;
    $scope.channels = Channel.query({
      $expand: 'Groups,Groups/Categorys'
    }, function() {
      if ($routeParams.id) {
        return Article.get({
          $filter: "PostId eq (guid'" + $routeParams.id + "')"
        }, function(data) {
          var category, item, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
          $scope.entity = data.value[0];
          $scope.sourceTitle = $scope.entity.Title;
          $scope.channelId = $scope.entity.Group.Channel.ChannelId;
          $scope.groupId = $scope.entity.Group.GroupId;
          _ref = $scope.entity.Categorys;
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            category = _ref[_i];
            _ref1 = $scope.getCategories();
            for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
              item = _ref1[_j];
              if (item.CategoryId === category.CategoryId) item.checked = true;
            }
          }
          if ($scope.entity.Tags) {
            $scope.tags = '';
            _ref2 = $scope.entity.Tags;
            for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
              item = _ref2[_k];
              $scope.tags += ',' + item.Name;
            }
            return $scope.tags = $scope.tags.substring(1);
          }
        });
      } else {
        $scope.entity = {};
        $scope.entity.PostId = UUID.generate();
        return isNew = true;
      }
    });
    $scope.getGroups = function() {
      var item, _i, _len, _ref;
      if ($scope.channels.value === void 0) return void 0;
      _ref = $scope.channels.value;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.ChannelId === $scope.channelId) return item.Groups;
      }
    };
    $scope.getCategories = function() {
      var item, _i, _len, _ref;
      if ($scope.getGroups() === void 0) return void 0;
      _ref = $scope.getGroups();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.GroupId === $scope.groupId) return item.Categorys;
      }
    };
    $scope.categorySelect = function(item) {
      return item.checked = item.checked ? true : false;
    };
    $scope.submit = function() {
      $scope.isSubmit = true;
      if (!$scope.channelValid()) return false;
      if (!$scope.groupValid()) return false;
      if (!$scope.categoryValid()) return false;
      if (!$scope.entity.Url) return false;
      if (!$scope.entity.Title) return false;
      if (!$scope.entity.Content) return false;
      if (!$scope.entity.Description) return false;
      if ($scope.files.length) {
        return uploadManager.upload();
      } else {
        return save();
      }
    };
    $scope.channelValid = function() {
      if ($scope.getGroups()) return true;
      return false;
    };
    $scope.groupValid = function() {
      if ($scope.getCategories()) return true;
      return false;
    };
    $scope.categoryValid = function() {
      var item, _i, _len, _ref;
      if (!$scope.getCategories()) return false;
      _ref = $scope.getCategories();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.checked) return true;
      }
      return false;
    };
    save = function() {
      var entity, item, _i, _j, _len, _len1, _ref, _ref1;
      entity = $scope.entity;
      entity.Group = {};
      entity.Group.GroupId = ((function() {
        var _i, _len, _ref, _results;
        _ref = $scope.getGroups();
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          if (item.GroupId === $scope.groupId) _results.push(item);
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
      if (entity.Source) entity.Source.SourceId = UUID.generate();
      if (isNew) {
        entity.PostId = UUID.generate();
        return Article.save(entity, function(data) {
          return $window.location.href = "/#!/post/" + data.Url;
        });
      } else {
        return Article.update({
          id: "(guid'" + entity.PostId + "')"
        }, entity, function(data) {
          return $window.location.href = "/#!/post/" + data.Url;
        });
      }
    };
    $scope.files = [];
    $scope.removeImg = function(file) {
      var deleteFile, f, _i, _len, _ref;
      _ref = $scope.files;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        f = _ref[_i];
        if (f.name === file.name) deleteFile = f;
      }
      $scope.files.splice($scope.files.indexOf(deleteFile), 1);
      return uploadManager.cancel(file);
    };
    $scope.removeServerImg = function() {
      return $scope.entity.Thumbnail = null;
    };
    $rootScope.$on("fileAdded", function(e, call) {
      $scope.files.push(call);
      return $scope.$apply();
    });
    $rootScope.$on("fileUploaded", function(e, call) {
      $scope.entity.Thumbnail = {
        ThumbnailId: UUID.generate(),
        Title: $scope.entity.Title,
        Url: call.result
      };
      return save();
    });
    return $scope.getUrl = function() {
      var s;
      $scope.TranslateUrl = true;
      $window.mycallback = function(response) {
        response = $.trim(response);
        response = response.toLowerCase();
        response = response.replace(/[^_a-zA-Z\d\s]/g, '');
        response = response.replace(/[\s]/g, "-");
        $scope.entity.Url = response;
        $scope.TranslateUrl = false;
        return $scope.$apply();
      };
      s = document.createElement("script");
      s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + $scope.entity.Title;
      return document.getElementsByTagName("head")[0].appendChild(s);
    };
  }
];
