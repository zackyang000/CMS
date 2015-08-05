angular.module('article-archives',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  articleFormat = (data) ->
    obj = []
    for post in data.value
      date = post.date?.format('yyyy-MM')
      obj[date]=[]  unless obj[date]
      obj[date].push(post)
    { date:key, posts:value } for key, value of obj when obj.hasOwnProperty key

  $routeProvider
    .when "/archives",
      templateUrl: "/app/article/archives/article-archives.tpl.html"
      controller: 'ArchivesCtrl'
      resolve:
        articles: ['$route', '$q', 'Articles', ($route, $q, Articles) ->
          deferred = $q.defer()
          Articles.list
            $select: 'title, url, date, category'
            $top: 1000
          ,(data)->
            deferred.resolve articleFormat(data)
          deferred.promise
        ]
])

.controller('ArchivesCtrl',
["$scope", 'articles',
($scope, articles) ->
  $scope.list = articles
])
