
angular.module('article-list-right-sidebar', ['resource.articles', 'resource.comments', 'ChannelServices']).controller('ArticleListRightSidebarCtrl', [
  "$scope", "$routeParams", "channel", function($scope, $routeParams, channel) {
    debugger;
    var setGroup;
    if ($routeParams.channel) {
      channel.get().then(function(channels) {
        var item, _i, _len;
        for (_i = 0, _len = channels.length; _i < _len; _i++) {
          item = channels[_i];
          if (item.Url.toLowerCase() === $routeParams.channel.toLowerCase()) {
            $scope.channel = item;
            break;
          }
        }
        return setGroup();
      });
    } else {
      channel.getdefault().then(function(channel) {
        $scope.channel = channel;
        return setGroup();
      });
    }
    return setGroup = function() {
      var item, _i, _len, _ref, _results;
      if ($routeParams.group) {
        _ref = $scope.channel.Groups;
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          if (item.Url.toLowerCase() === $routeParams.group.toLowerCase()) {
            $scope.group = item;
            break;
          } else {
            _results.push(void 0);
          }
        }
        return _results;
      }
    };
  }
]);
