angular.module('article-archives',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/archives",
      templateUrl: "/app/article/archives/article-archives.tpl.html"
      controller: 'ArticleArchivesCtrl'
      title: 'Archives'
      resolve:
        archives: ['$q','Articles',($q,Articles)->
          deferred = $q.defer()
          Articles.query
            $top: 10000
            $select: 'title,url,date'
          ,(articles) ->
            obj = []
            for post in articles.value
              date = new Date(post.date)
              year = date.getFullYear()
              month = date.getMonth() + 1
              month = '0' + month  if month < 10
              date = "#{year}-#{month}"
              unless obj[date]
                obj[date]=[]
              obj[date].push(post)
            result=[]
            for key,value of obj
              if obj.hasOwnProperty key
                result.push
                  date:key
                  posts:value
            deferred.resolve result
          deferred.promise
        ]
])

.controller('ArticleArchivesCtrl',
["$scope", "archives",
($scope, archives) ->
  $scope.list = archives
])