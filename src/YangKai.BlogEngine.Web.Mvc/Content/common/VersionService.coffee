angular.module("VersionServices", [])
.factory "VersionService", ['$http','$q',($http, $q) ->
  get: ->
    deferred = $q.defer()
    if @data
      deferred.resolve @data
    else
      self=this
      $http.get("/version.txt").success(
        (data) ->
          list=data.match(/([^\r\n])+/g)
          versions=[]
          for item in list
            if item.indexOf('v')==0
              info=item.split(' ')
              if info.length==2
                begin=true
                versions.push
                  ver:info[0]
                  date:info[1]
                  content:[]
            else
              if begin
                versions[versions.length-1].content.push item
          self.data=versions
          deferred.resolve self.data
      ,
        (data) ->
          deferred.reject data
      )
    deferred.promise
]