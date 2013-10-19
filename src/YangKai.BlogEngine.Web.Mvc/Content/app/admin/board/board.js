
angular.module('admin-board', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/content/app/admin/board/board.tpl.html",
      controller: 'BoardCtrl'
    });
  }
]).controller('BoardCtrl', ["$scope", "$routeParams", "$location", "Article", function($scope, $routeParams, $location, Article) {}]);
