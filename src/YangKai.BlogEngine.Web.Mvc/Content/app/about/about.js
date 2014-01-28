
angular.module('about', []).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/about", {
      templateUrl: "/Content/app/about/about-newegg-bts.tpl.html",
      controller: 'AboutCtrl',
      title: 'About',
      resolve: {
        members: [
          '$q', '$http', function($q, $http) {
            var deferred;
            deferred = $q.defer();
            $http.get('/Content/data/about-newegg-bts.js').success(function(data) {
              return deferred.resolve(data);
            });
            return deferred.promise;
          }
        ]
      }
    });
  }
]).controller('AboutCtrl', [
  "$scope", "members", function($scope, members) {
    var i, item, rowId, viewMembers, _i, _len;
    viewMembers = [];
    for (i = _i = 0, _len = members.length; _i < _len; i = ++_i) {
      item = members[i];
      rowId = Math.floor(i / 4);
      if (!viewMembers[rowId]) {
        viewMembers[rowId] = [];
      }
      viewMembers[rowId].push(item);
    }
    return $scope.members = viewMembers;
  }
]);
