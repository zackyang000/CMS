
angular.module('article-detail-right-sidebar', ['resource.articles', 'resource.comments']).controller('ArticleDetailRightSidebarCtrl', [
  "$scope", "$routeParams", function($scope, $routeParams) {
    var item, _i, _len, _ref;
    _ref = $scope.Channels;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      item = _ref[_i];
      if (item.Name.toLowerCase() === $scope.item.Group.Channel.Name.toLowerCase()) {
        $scope.channel = item;
        break;
      }
    }
    return $scope.group = $scope.item.Group;
  }
]);
