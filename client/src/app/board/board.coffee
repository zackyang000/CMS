angular.module('board',['resource.messages'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/board",
      templateUrl: "/app/board/board.tpl.html"
      controller: 'BoardCtrl'
      title: 'Message Boards'
      resolve:
        messages: ['$q','Message',($q,Message)->
          deferred = $q.defer()
          debugger
          Message.queryOnce 
            $filter:'IsDeleted eq false'
          , (data) -> 
            for item in data.value
              if !item.Avatar
                if item.Email
                  item.Avatar='http://www.gravatar.com/avatar/' + md5(item.Email) 
                else
                  item.Avatar='/img/avatar.png'
            deferred.resolve data.value
          deferred.promise
        ]
])

.controller('BoardCtrl',
["$scope","$translate","progressbar","Message","messages" ,"messager", "context"
($scope, $translate, progressbar, Message, messages, messager, context) ->
  $scope.entity=
    Author:context.account.name
    Email:context.account.email
    Url:context.account.url
  $scope.editmode = !context.account.name
  $scope.isAdmin = context.auth.admin

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
      $scope.list.unshift(data)
      $scope.entity.Content=""
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
      context.account =
        name: $scope.entity.Author
        email: $scope.entity.Email
        url: $scope.entity.Url
    ,(error)->
      $scope.submitted=false
      $scope.loading = ""

  $scope.remove = (item) ->
    message.confirm ->
      Message.remove id:"(guid'#{item.BoardId}')",->
        item.IsDeleted=true
        messager.success "Message has been removed."
])