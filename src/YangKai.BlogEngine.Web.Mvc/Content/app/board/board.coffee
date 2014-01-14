angular.module('board',['resource.messages'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/board",
      templateUrl: "/Content/app/board/board.tpl.html"
      controller: 'BoardCtrl'
      title: 'Message Boards'
      resolve:
        messages: ['$q','Message',($q,Message)->
          deferred = $q.defer()
          Message.queryOnce 
            $filter:'IsDeleted eq false'
          , (data) -> 
            for item in data
              if !item.Avatar
                if item.Email
                  item.Avatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
                else
                  item.Avatar='/Content/img/avatar.png'
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('BoardCtrl',
["$scope","$translate","progressbar","Message","messages","account" 
($scope,$translate,progressbar,Message,messages,account) ->
  $scope.$parent.showBanner=false

  account.get().then (data) ->
    $scope.entity=
      Author:data.UserName
      Email:data.Email
      Url:data.Url
    $scope.editmode=!data.UserName

  $scope.list = messages

  $scope.save = () ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    progressbar.start()
    $scope.loading = $translate("global.post")
    $scope.entity.BoardId=UUID.generate()
    Message.save $scope.entity
    ,(data)->
      message.success $translate("board.complete")
      $scope.list.unshift(data)
      $scope.entity.Content=""
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
    ,(error)->
      $scope.submitted=false
      $scope.loading = ""

  $scope.remove = (item) ->
    message.confirm ->
      Message.remove id:"(guid'#{item.BoardId}')",->
        item.IsDeleted=true
        message.success "Message has been removed."
])