
angular.module('board', ['MessageServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/Content/app/board/board.tpl.html",
      controller: 'BoardCtrl'
    });
  }
]).controller('BoardCtrl', [
  "$scope", "$translate", "progressbar", "Message", function($scope, $translate, progressbar, Message) {
    $scope.$parent.title = 'Message Boards';
    $scope.$parent.showBanner = false;
    $scope.entity = {};
    $scope.$watch('User', function() {
      if ($scope.User) {
        $scope.entity.Author = $scope.User.UserName;
        $scope.entity.Email = $scope.User.Email;
        $scope.entity.Url = $scope.User.Url;
        $scope.AuthorForDisplay = $scope.User.UserName;
        return $scope.editmode = $scope.User.UserName === '' || !($scope.User.UserName != null);
      }
    });
    $scope.loading = $translate("global.loading");
    $scope.list = Message.query({
      $filter: 'IsDeleted eq false'
    }, function() {
      var item, _i, _len, _ref;
      _ref = $scope.list.value;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (!item.Avatar) {
          if (item.Email) {
            item.Avatar = 'http://www.gravatar.com/avatar/' + md5(item.Email);
          } else {
            item.Avatar = '/Content/img/avatar.png';
          }
        }
      }
      return $scope.loading = "";
    });
    $scope.save = function() {
      progressbar.start();
      $scope.loading = $translate("global.post");
      $scope.entity.BoardId = UUID.generate();
      return Message.save($scope.entity, function(data) {
        message.success($translate("board.complete"));
        $scope.list.value.unshift(data);
        $scope.entity.Content = "";
        $scope.AuthorForDisplay = data.Author;
        $scope.editmode = false;
        angular.resetForm($scope, 'form');
        progressbar.complete();
        return $scope.loading = "";
      });
    };
    return $scope.remove = function(item) {
      return message.confirm(function() {
        return Message.remove({
          id: "(guid'" + item.BoardId + "')"
        }, function() {
          item.IsDeleted = true;
          return message.success("Message has been removed.");
        });
      });
    };
  }
]);
