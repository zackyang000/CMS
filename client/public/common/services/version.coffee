angular.module("zy.services.version", [])
.factory "version", ['$http','$q',($http, $q) ->
  get: ->
    deferred = $q.defer()
    $http.get("/data/version.txt",cache:true)
      .success (data) ->
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
        deferred.resolve versions
      .error (data) ->
        deferred.reject data
    deferred.promise
]