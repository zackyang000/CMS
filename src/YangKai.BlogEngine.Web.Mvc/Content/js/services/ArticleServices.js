
angular.module("ArticleServices", ["ngResource"]).factory("Article", [
  '$resource', function($resource) {
    return $resource("/odata/Article:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'CreateDate desc',
          $expand: 'Categorys,Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin',
          $inlinecount: 'allpages'
        }
      },
      get: {
        method: "GET",
        params: {
          $top: 1,
          $expand: 'Categorys,Tags,Thumbnail,Source,Group,Group/Channel,PubAdmin,Comments'
        }
      },
      update: {
        method: "PUT"
      }
    });
  }
]);
