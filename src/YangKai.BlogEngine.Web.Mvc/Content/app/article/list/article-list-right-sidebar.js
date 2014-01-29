
angular.module('article-list-right-sidebar', ['resource.articles', 'resource.comments', 'ChannelServices']).controller('ArticleListRightSidebarCtrl', [
  "$scope", "$routeParams", "channel", function($scope, $routeParams, channel) {
    var item, _i, _len, _ref, _results;
    channel.get().then(function(channels) {
      var item, _i, _len, _results;
      if ($routeParams.channel) {
        _results = [];
        for (_i = 0, _len = channels.length; _i < _len; _i++) {
          item = channels[_i];
          if (item.Name.toLowerCase() === $routeParams.channel.toLowerCase()) {
            $scope.channel = item;
            break;
          } else {
            _results.push(void 0);
          }
        }
        return _results;
      } else {
        debugger;
        return $scope.channel = channels[0];
      }
    });
    if ($routeParams.group) {
      _ref = $scope.channel.Groups;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.Name.toLowerCase() === $routeParams.group.toLowerCase()) {
          $scope.group = item;
          break;
        } else {
          _results.push(void 0);
        }
      }
      return _results;
    }
  }
]);
