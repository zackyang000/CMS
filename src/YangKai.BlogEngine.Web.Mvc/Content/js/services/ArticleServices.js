
angular.module("ArticleServices", ["ngResource"]).factory("Article", [
  '$resource', function($resource) {
    return $resource("/odata/Post:id/:action", {
      id: '@id',
      action: '@action'
    }, {
      query: {
        method: "GET",
        params: {
          $orderby: 'CreateDate desc',
          $expand: 'Categorys,Tags,Group,Group/Channel',
          $inlinecount: 'allpages'
        }
      },
      nav: {
        method: "GET",
        params: {
          action: "nav"
        },
        isArray: true
      },
      related: {
        method: "GET",
        params: {
          action: "related"
        },
        isArray: true
      }
    });
  }
]);
