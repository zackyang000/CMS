
angular.module('issue', ['IssueServices']).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/issue", {
      templateUrl: "/content/app/issue/issue.tpl.html",
      controller: 'IssueCtrl'
    });
  }
]).controller('IssueCtrl', [
  "$scope", "$routeParams", "$location", "Issue", function($scope, $routeParams, $location, Issue) {
    $scope.$parent.title = 'Issues';
    $scope.$parent.showBanner = false;
    $scope.projects = ['API Portal', 'API Framework', 'ServiceStack.Text', 'Framework API/ API SDK', 'Oversea WCF Framework', 'Framework Tools', 'Auth Service', 'Gateway', 'Oversea Data Access', 'Cassandra Adapter', 'Document Tool', 'Common API', 'API Notify', 'Newegg Central Framework', 'HR Tools'].sort();
    $scope.get = function() {
      return Issue.query(function(data) {
        var item, key, project, statu, user, value, _i, _j, _len, _len1, _ref, _ref1, _results;
        _ref = data.value;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          item = _ref[_i];
          item.CreateDate = moment(item.CreateDate).fromNow();
        }
        $scope.list = data.value;
        statu = {};
        project = {};
        user = {};
        _ref1 = $scope.list;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          item = _ref1[_j];
          if (!statu[item.Statu]) statu[item.Statu] = 0;
          statu[item.Statu]++;
          if (!project[item.Project]) project[item.Project] = 0;
          project[item.Project]++;
          if (!user[item.Author]) user[item.Author] = 0;
          user[item.Author]++;
        }
        $scope.statu = [];
        for (key in statu) {
          value = statu[key];
          if (statu.hasOwnProperty(key)) {
            $scope.statu.push({
              key: key,
              value: value
            });
          }
        }
        $scope.project = [];
        for (key in project) {
          value = project[key];
          if (project.hasOwnProperty(key)) {
            $scope.project.push({
              key: key,
              value: value
            });
          }
        }
        $scope.user = [];
        _results = [];
        for (key in user) {
          value = user[key];
          if (user.hasOwnProperty(key)) {
            _results.push($scope.user.push({
              key: key,
              value: value
            }));
          } else {
            _results.push(void 0);
          }
        }
        return _results;
      });
    };
    $scope.add = function() {
      $scope.entity = {};
      return $scope.editDialog = true;
    };
    $scope.save = function() {
      var entity;
      $scope.loading = 'save';
      entity = $scope.entity;
      entity.IssueId = UUID.generate();
      entity.Statu = 'Open';
      return Issue.update({
        id: "(guid'" + entity.IssueId + "')"
      }, entity, function(data) {
        message.success("Create issue successfully.");
        $scope.editDialog = false;
        $scope.loading = "";
        return $scope.get();
      });
    };
    $scope.handle = function() {};
    return $scope.get();
  }
]);
