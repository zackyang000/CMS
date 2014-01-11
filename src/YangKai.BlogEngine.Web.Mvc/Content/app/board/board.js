
angular.module('board', ['resource.messages']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/Content/app/board/board.tpl.html",
      controller: 'BoardCtrl'
    });
  }
]).controller('BoardCtrl', [
  "$scope", "$translate", "progressbar", "Message", "account", function($scope, $translate, progressbar, Message, account) {
    $scope.$parent.title = 'Message Boards';
    $scope.$parent.showBanner = false;
    account.get().then(function(data) {
      $scope.entity = {
        Author: data.UserName,
        Email: data.Email,
        Url: data.Url
      };
      return $scope.editmode = !data.UserName;
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
      $scope.submitted = true;
      if ($scope.form.$invalid) {
        return;
      }
      progressbar.start();
      $scope.loading = $translate("global.post");
      $scope.entity.BoardId = UUID.generate();
      return Message.save($scope.entity, function(data) {
        message.success($translate("board.complete"));
        $scope.list.value.unshift(data);
        $scope.entity.Content = "";
        progressbar.complete();
        $scope.submitted = false;
        return $scope.loading = "";
      }, function(error) {
        $scope.submitted = false;
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
