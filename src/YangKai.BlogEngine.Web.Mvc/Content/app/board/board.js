
angular.module('board', ['resource.messages']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/board", {
      templateUrl: "/Content/app/board/board.tpl.html",
      controller: 'BoardCtrl',
      title: 'Message Boards',
      resolve: {
        messages: [
          '$q', 'Message', function($q, Message) {
            var deferred;
            deferred = $q.defer();
            Message.queryOnce({
              $filter: 'IsDeleted eq false'
            }, function(data) {
              var item, _i, _len;
              for (_i = 0, _len = data.length; _i < _len; _i++) {
                item = data[_i];
                if (!item.Avatar) {
                  if (item.Email) {
                    item.Avatar = 'http://www.gravatar.com/avatar/' + md5(item.Email);
                  } else {
                    item.Avatar = '/Content/img/avatar.png';
                  }
                }
              }
              return deferred.resolve(data.value);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('BoardCtrl', [
  "$scope", "$translate", "progressbar", "Message", "messages", "account", function($scope, $translate, progressbar, Message, messages, account) {
    account.get().then(function(data) {
      $scope.entity = {
        Author: data.UserName,
        Email: data.Email,
        Url: data.Url
      };
      return $scope.editmode = !data.UserName;
    });
    $scope.list = messages;
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
        $scope.list.unshift(data);
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
