var ArticleDetailController;

ArticleDetailController = [
  "$scope", "$routeParams", "Article", "Channel", function($scope, $routeParams, Article, Channel) {
    $scope.id = $routeParams.id;
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
    return $scope.getCategories = function() {
      var item, _i, _len, _ref;
      if ($scope.getGroups() === void 0) {
        return void 0;
      }
      _ref = $scope.getGroups();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        debugger;
        if (item.GroupId === $scope.groupId) {
          return item.Categorys;
        }
      }
    };
  }
];
