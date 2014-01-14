
angular.module('issue', ['resource.issues']).filter('filterByDate', function() {
  return function(input, filterDate) {
    var item, list, _i, _len;
    if (!input) {
      return input;
    }
    list = [];
    for (_i = 0, _len = input.length; _i < _len; _i++) {
      item = input[_i];
      if (filterDate === '') {
        list.push(item);
      }
      if (filterDate === 'day' && moment(new Date()).format('YYYY-MM-DD') === moment(item.CreateDate).format('YYYY-MM-DD')) {
        list.push(item);
      }
      if (filterDate === 'week' && moment(new Date()).diff(item.CreateDate, 'days') <= 7) {
        list.push(item);
      }
      if (filterDate === 'month' && moment(new Date()).diff(item.CreateDate, 'days') <= 30) {
        list.push(item);
      }
    }
    return list;
  };
}).config([
  "$routeProvider", function($routeProvider) {
    return $routeProvider.when("/issue", {
      templateUrl: "/content/app/issue/issue.tpl.html",
      controller: 'IssueCtrl',
      title: 'Issues'
    });
  }
]).controller('IssueCtrl', [
  "$scope", "$translate", "$routeParams", "$location", "Issue", function($scope, $translate, $routeParams, $location, Issue) {
    $scope.projects = ['API Portal', 'API Framework', 'ServiceStack.Text', 'Framework API/ API SDK', 'Oversea WCF Framework', 'Framework Tools', 'Auth Service', 'Gateway', 'Oversea Data Access', 'Cassandra Adapter', 'Document Tool', 'Common API', 'API Notify', 'Newegg Central Framework', 'HR Tools'].sort();
    $scope.get = function() {
      $scope.loading = $translate("global.loading");
      return Issue.query(function(data) {
        $scope.list = data.value;
        $scope.setGroup();
        return $scope.loading = "";
      });
    };
    $scope.setGroup = function() {
      var date, item, key, project, statu, value, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2, _results;
      statu = {};
      project = {};
      date = {};
      $scope.count = {
        statu: 0,
        project: 0,
        date: 0
      };
      statu['Open'] = 0;
      statu['Closed'] = 0;
      statu['Cancel'] = 0;
      _ref = $scope.list;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        if ($scope.filter.Project === '' || item.Project === $scope.filter.Project) {
          if ($scope.filterDate === '') {
            if (!statu[item.Statu]) {
              statu[item.Statu] = 0;
            }
            statu[item.Statu]++;
            $scope.count.statu++;
          }
          if ($scope.filterDate === 'day' && moment(new Date()).format('YYYY-MM-DD') === moment(item.CreateDate).format('YYYY-MM-DD')) {
            if (!statu[item.Statu]) {
              statu[item.Statu] = 0;
            }
            statu[item.Statu]++;
            $scope.count.statu++;
          }
          if ($scope.filterDate === 'week' && moment(new Date()).diff(item.CreateDate, 'days') <= 7) {
            if (!statu[item.Statu]) {
              statu[item.Statu] = 0;
            }
            statu[item.Statu]++;
            $scope.count.statu++;
          }
          if ($scope.filterDate === 'month' && moment(new Date()).diff(item.CreateDate, 'days') <= 30) {
            if (!statu[item.Statu]) {
              statu[item.Statu] = 0;
            }
            statu[item.Statu]++;
            $scope.count.statu++;
          }
        }
      }
      _ref1 = $scope.list;
      for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
        item = _ref1[_j];
        if ($scope.filter.Statu === '' || item.Statu === $scope.filter.Statu) {
          if ($scope.filterDate === '') {
            if (!project[item.Project]) {
              project[item.Project] = 0;
            }
            project[item.Project]++;
            $scope.count.project++;
          }
          if ($scope.filterDate === 'day' && moment(new Date()).format('YYYY-MM-DD') === moment(item.CreateDate).format('YYYY-MM-DD')) {
            if (!project[item.Project]) {
              project[item.Project] = 0;
            }
            project[item.Project]++;
            $scope.count.project++;
          }
          if ($scope.filterDate === 'week' && moment(new Date()).diff(item.CreateDate, 'days') <= 7) {
            if (!project[item.Project]) {
              project[item.Project] = 0;
            }
            project[item.Project]++;
            $scope.count.project++;
          }
          if ($scope.filterDate === 'month' && moment(new Date()).diff(item.CreateDate, 'days') <= 30) {
            if (!project[item.Project]) {
              project[item.Project] = 0;
            }
            project[item.Project]++;
            $scope.count.project++;
          }
        }
      }
      date['day'] = 0;
      date['week'] = 0;
      date['month'] = 0;
      _ref2 = $scope.list;
      for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
        item = _ref2[_k];
        if ($scope.filter.Statu === '' || item.Statu === $scope.filter.Statu) {
          if ($scope.filter.Project === '' || item.Project === $scope.filter.Project) {
            if (moment(item.CreateDate).format('YYYY-MM-DD') === moment(new Date()).format('YYYY-MM-DD')) {
              date['day']++;
            }
            if (moment(new Date()).diff(item.CreateDate, 'days') <= 7) {
              date['week']++;
            }
            if (moment(new Date()).diff(item.CreateDate, 'days') <= 30) {
              date['month']++;
            }
            $scope.count.date++;
          }
        }
      }
      $scope.statu = statu;
      $scope.date = date;
      $scope.project = [];
      _results = [];
      for (key in project) {
        value = project[key];
        if (project.hasOwnProperty(key)) {
          _results.push($scope.project.push({
            key: key,
            value: value
          }));
        } else {
          _results.push(void 0);
        }
      }
      return _results;
    };
    $scope.add = function() {
      $scope.entity = {};
      return $scope.editDialog = true;
    };
    $scope.save = function() {
      var entity;
      $scope.loading = $translate("global.post");
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
    $scope.edit = function(item) {
      item.show = true;
      item.edit = !item.edit;
      item.title = item.Title;
      item.content = item.Content;
      item.result = item.Result;
      return item.statu = item.Statu;
    };
    $scope.handle = function(item) {
      debugger;
      var entity;
      item.Title = item.title;
      item.Content = item.content;
      item.Result = item.result;
      item.Statu = item.statu;
      $scope.loading = 'save';
      entity = angular.copy(item);
      delete entity.edit;
      delete entity.show;
      delete entity.date;
      delete entity.title;
      delete entity.content;
      delete entity.result;
      delete entity.statu;
      return Issue.update({
        id: "(guid'" + entity.IssueId + "')"
      }, entity, function(data) {
        message.success("Save issue successfully.");
        $scope.editDialog = false;
        $scope.loading = "";
        return $scope.get();
      });
    };
    return $scope.get();
  }
]);
