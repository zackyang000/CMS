angular.module("zy.services.odataResource", ['ngResource'])

.factory("odataResource", ['$resource', function($resource) {
  return function(url, actions) {
    entityUrl = url + "(:id)/:action"
    actions = actions || {};

    if (actions.list) {
      actions.list.url = url;
    } else {
      actions.list = { method: "GET", url: url };
    }
    if (actions.post) {
      actions.post.url = url;
    } else {
      actions.post = { method: "POST", url: url };
    }
    if (actions.put) {
      actions.put.url = url;
    } else {
      actions.put = { method: "PUT", url: entityUrl };
    }
    if (actions.get) {
      actions.get.url = entityUrl;
    } else {
      actions.get = { method: "GET", url: entityUrl };
    }
    if (actions.delete) {
      actions.delete.url = entityUrl;
    } else {
      actions.delete = { method: "DELETE", url: entityUrl };
    }

    return $resource('', { id: '@id' }, actions);
  }
}]);

