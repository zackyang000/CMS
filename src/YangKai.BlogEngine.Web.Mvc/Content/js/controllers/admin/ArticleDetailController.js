var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", "Channel", function($scope, $routeParams, Article, Channel) {
    $scope.id = $routeParams.id;
    $scope.channels = Channel.query({
      $expand: 'Groups,Groups/Categorys'
    });
    $scope.entity = {};
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
    return $scope.submit = function() {
      var item;
      $scope.entity.Group = ((function() {
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
      })())[0];
      debugger;
      return Article.save($scope.entity);
    };
  }
];
