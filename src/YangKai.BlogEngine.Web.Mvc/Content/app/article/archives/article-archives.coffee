angular.module('article-archives',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/archives",
      templateUrl: "/Content/app/article/archives/article-archives.tpl.html"
      controller: 'ArticleArchivesCtrl'
      title: 'Archives'
      resolve:
        archives: ['$q','Channel',($q,Channel)->
          deferred = $q.defer()
          Channel.queryOnce
            $select:'Name,Url,Groups/Name,Groups/Url,Groups/IsDeleted,Groups/Posts/Title,Groups/Posts/Url,Groups/Posts/PubDate,Groups/Posts/IsDeleted'
            $expand:'Groups/Posts'
            $filter:'IsDeleted eq false'
          , (data) -> 
            obj={}
            for channel in data.value when !channel.IsDeleted
              for group in channel.Groups when !group.IsDeleted
                for post in group.Posts when !post.IsDeleted
                  date=moment(post.PubDate).format('YYYY-MM')
                  unless obj[date]
                    obj[date]=[]
                  post.group=group.Name
                  post.channel=channel.Name
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
["$scope","$translate","archives", 
($scope,$translate,archives) ->
  $scope.$parent.showBanner=false
  $scope.list=archives
])