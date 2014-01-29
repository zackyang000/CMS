
angular.module('article-list-right-sidebar', ['resource.articles', 'resource.comments']).controller('ArticleListRightSidebarCtrl', [
  "$scope", "$routeParams", function($scope, $routeParams) {
    var item, _i, _j, _len, _len1, _ref, _ref1, _results;
    _ref = $scope.Channels;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      item = _ref[_i];
      if (item.Name.toLowerCase() === $routeParams.channel.toLowerCase()) {
        $scope.channel = item;
        break;
      }
    }
    if ($routeParams.group) {
      _ref1 = $scope.channel.Groups;
      _results = [];
      for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
        item = _ref1[_j];
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
