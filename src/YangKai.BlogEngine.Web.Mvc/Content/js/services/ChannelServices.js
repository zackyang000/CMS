
angular.module("ChannelServices", ["ngResource"]).factory("Channel", [
  '$resource', function($resource) {
    return $resource("/odata/Channel:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'Url',
          $inlinecount: 'allpages',
          $filter: 'IsDeleted eq false'
        }
      },
      edit: {
        method: "PUT"
      },
      archives: {
        method: "GET",
        params: {
          $expand: 'Groups/Posts',
          $filter: 'IsDeleted eq false',
          $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/Posts/Title,Groups/Posts/Url,Groups/Posts/PubDate,Groups/Posts/IsDeleted'
        }
      },
      categories: {
        method: "GET",
        params: {
          $expand: 'Groups/Categorys',
          $filter: 'IsDeleted eq false',
          $select: 'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/Categorys/Name,Groups/Categorys/Url,Groups/Categorys/IsDeleted'
        }
      }
    });
  }
]);
