angular.module('board',['resource.articles'])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/board",
      templateUrl: "/app/board/board.tpl.html"
      controller: 'BoardCtrl'
      title: 'Message Boards'
      resolve:
        messages: ['$q','Comments',($q,Comments)->
          deferred = $q.defer()
          Comments.query
            id: 'message'
          , (data) ->
            deferred.resolve data
          deferred.promise
        ]
])

.controller('BoardCtrl',
["$scope", "$translate", "messages", "context", "progressbar", "Comments"
($scope, $translate, messages, context, progressbar, Comments) ->

  $scope.messages = messages

  #初始化新评论
  $scope.entity=
    author : context.account.name
    email : context.account.email
    url : context.account.url
    type : 'message'
  $scope.editmode = !context.account.name
  $scope.isAdmin = context.auth.admin

  #提交评论
  $scope.save = ->
    $scope.submitted=true
    if $scope.form.$invalid
      return

    progressbar.start()
    $scope.loading = $translate("global.post")
    Comments.save $scope.entity
    , (data)->
      $scope.messages.push(data)
      $scope.entity.content = ""
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""
      context.account =
        name: $scope.entity.author
        email: $scope.entity.email
        url: $scope.entity.url
    ,(error)->
      progressbar.complete()
      $scope.submitted=false
      $scope.loading = ""

  $scope.remove = (item) ->
    messager.confirm ->
      Message.remove id:"(guid'#{item.BoardId}')",->
        item.IsDeleted=true
        messager.success "Message has been removed."
])