
angular.module('article-archives', ['resource.articles']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/archives", {
      templateUrl: "/Content/app/article/archives/article-archives.tpl.html",
      controller: 'ArticleArchivesCtrl',
      title: 'Archives',
      resolve: {
        archives: [
          '$q', 'Channel', function($q, Channel) {
            var deferred;
            deferred = $q.defer();
            Channel.queryOnce({
              $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/Posts/Title,Groups/Posts/Url,Groups/Posts/PubDate,Groups/Posts/IsDeleted',
              $expand: 'Groups/Posts',
              $filter: 'IsDeleted eq false'
            }, function(data) {
              var channel, date, group, key, obj, post, result, value, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2;
              obj = {};
              _ref = data.value;
              for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                channel = _ref[_i];
                if (!channel.IsDeleted) {
                  _ref1 = channel.Groups;
                  for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                    group = _ref1[_j];
                    if (!group.IsDeleted) {
                      _ref2 = group.Posts;
                      for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                        post = _ref2[_k];
                        if (!(!post.IsDeleted)) {
                          continue;
                        }
                        date = moment(post.PubDate).format('YYYY-MM');
                        if (!obj[date]) {
                          obj[date] = [];
                        }
                        post.group = group.Name;
                        post.channel = channel.Name;
                        obj[date].push(post);
                      }
                    }
                  }
                }
              }
              result = [];
              for (key in obj) {
                value = obj[key];
                if (obj.hasOwnProperty(key)) {
                  result.push({
                    date: key,
                    posts: value
                  });
                }
              }
              return deferred.resolve(result);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('ArticleArchivesCtrl', [
  "$scope", "$translate", "archives", function($scope, $translate, archives) {
    $scope.$parent.showBanner = false;
    return $scope.list = archives;
  }
]);
