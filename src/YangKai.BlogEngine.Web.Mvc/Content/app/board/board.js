
angular.module('board', ['MessageServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/Content/app/board/board.tpl.html",
      controller: 'BoardCtrl'
    });
  }
]).controller('BoardCtrl', [
  "$scope", "progressbar", "Message", function($scope, progressbar, Message) {
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
    $scope.loading = "Loading";
    $scope.list = Message.query({
      $filter: 'IsDeleted eq false'
    }, function() {
      var item, _i, _len, _ref;
      _ref = $scope.list.value;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if (item.Email) {
          item.Gravatar = 'http://www.gravatar.com/avatar/' + md5(item.Email);
        } else {
          item.Gravatar = '/Content/img/avatar.png';
        }
      }
      return $scope.loading = "";
    });
    $scope.save = function() {
      progressbar.start();
      $scope.submitting = true;
      $scope.entity.BoardId = UUID.generate();
      return Message.save($scope.entity, function(data) {
        message.success("Message has been submitted.");
        $scope.list.value.unshift(data);
        $scope.entity.Content = "";
        $scope.AuthorForDisplay = data.Author;
        $scope.editmode = false;
        angular.resetForm($scope, 'form');
        $scope.submitting = false;
        return progressbar.complete();
      });
    };
    return $scope.remove = function(item) {
      return Message.remove({
        id: "(guid'" + item.BoardId + "')"
      }, function() {
        item.IsDeleted = true;
        return message.success("Message has been removed.");
      });
    };
  }
]);
