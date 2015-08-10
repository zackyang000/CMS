angular.module("zy.services.odataResource", ['ngResource'])

.factory("odataResource", ['$resource', function($resource) {
  return function(url, actions) {
    actions = actions || {};
    entityUrl = url + "(:id)/:action"

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
    return $resource(entityUrl, { id: '@id' }, actions);
  }
}]);
